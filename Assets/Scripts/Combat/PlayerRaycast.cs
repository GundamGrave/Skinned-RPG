using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    private PlayerStats ps;
    [SerializeField] LayerMask mask;
    [SerializeField] LayerMask mask2;
    private UnitInformation[] GOs;

    public GameObject targetLoc;
    public bool targeting;
    public bool radiusMode;
    public Skill skill;
    public CardUI cUI;
    public DeckUI dUI;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        targetLoc = new GameObject();
        targetLoc.AddComponent<SphereCollider>();
        targetLoc.GetComponent<SphereCollider>().isTrigger = true;
        targetLoc.AddComponent<TargetChecker>();
        targetLoc.transform.parent = transform;
        targetLoc.name = "Target Location";
        targetLoc.AddComponent<Rigidbody>();
        targetLoc.GetComponent<Rigidbody>().isKinematic = true;
        targetLoc.GetComponent<Rigidbody>().useGravity = false;

        ps = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targeting)
        {
            ps.movement.canMove = false;
            skill = ps.SelectedSkill;
            if (skill == null)
                return;

            if (radiusMode) // Targeting enemies in a radius
            {
                targetLoc.transform.position = something() + new Vector3(0, 0.15f, 0);
                targetLoc.DrawCircle(skill.Radius, 0.1f);
                targetLoc.GetComponent<SphereCollider>().radius = skill.Radius;
                if (Input.GetButtonDown("Fire1"))
                {
                    GOs = targetLoc.GetComponent<TargetChecker>().UIs.ToArray();
                    foreach (UnitInformation ui in GOs)
                    {
                        ui.ModifyStat(UnitInformation.Stats.CurrentHealth, -skill.TargetDamage);
                        foreach (Status s in skill.TargetStatuses)
                        {
                            ui.NewStatus(s);
                        }
                    }

                    ps.ModifyStat(UnitInformation.Stats.CurrentHealth, -skill.PlayerDamage);
                    foreach (Status s in skill.PlayerStatuses)
                    {
                        ps.NewStatus(s);
                    }

                    //Destroy card and stuff
                    ps.ModifyStat(UnitInformation.Stats.ActionPoints, -skill.Cost);
                    dUI.RemoveCard(cUI);
                    ps.gameObject.GetComponent<PlayerHand>().RemoveCard(index);
                    Destroy(cUI.gameObject);
                    // Unselect Skill and no more targeting
                    ps.SelectedSkill = null;
                    targeting = false;
                }
            }

            else // Select specific targets in a radius
            {
                targetLoc.transform.position = transform.position;
                targetLoc.DrawCircle(skill.Range, 0.1f);
                targetLoc.GetComponent<SphereCollider>().radius = skill.Range;
                GOs = targetLoc.GetComponent<TargetChecker>().UIs.ToArray();
                if (Input.GetButtonDown("Fire1"))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask2))
                    {
                        if (hit.transform.gameObject.GetComponent<UnitInformation>() == null)
                            return;
                        foreach (UnitInformation ui in GOs)
                        {
                            if (ui == hit.transform.gameObject.GetComponent<UnitInformation>()) {
                                foreach (Status s in skill.TargetStatuses)
                                {
                                    ui.NewStatus(s);
                                }

                                ui.ModifyStat(UnitInformation.Stats.CurrentHealth, -skill.TargetDamage);
                            }

                        }
                        //destroy card and stuff
                        ps.ModifyStat(UnitInformation.Stats.ActionPoints, -skill.Cost);
                        dUI.RemoveCard(cUI);
                        ps.gameObject.GetComponent<PlayerHand>().RemoveCard(index);
                        Destroy(cUI.gameObject);
                        // Unselect Skill and no more targeting
                        ps.SelectedSkill = null;
                        targeting = false;
                    }
                }
            }
        }
        else if (!targeting && ps.inCombat && ps.myTurn)
        {
            ps.movement.canMove = true;
        }
        else if (!targeting && !ps.inCombat)
        {
            ps.movement.canMove = true;
        }
        if (!targeting)
        {
            targetLoc.transform.position = transform.position;
            targetLoc.DrawCircle(0f, 0f);
        }
    }

    private Vector3 something() // hahhah need to name this properly (gets location of where the mouse is hovering)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (radiusMode)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                Vector3 location = hit.point;
                return location;
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask2))
            {
                Vector3 location = hit.point;
                return location;
            }
        }

        return transform.position;
    }
}
