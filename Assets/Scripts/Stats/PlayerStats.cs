using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitInformation
{
    public int str, intel, init;

    public bool inCombat;
    private Movement movement;

    public bool targetMode;
    public GameObject targetLoc;
    [SerializeField] LayerMask mask;

    public Skill SelectedSkill;

    public override void InitializeStats()
    {
        base.InitializeStats();

       SetStat(Stats.Experience, 0);
       
    }

    public override void Start()
    {
        base.Start();

        movement = GetComponent<Movement>();

        targetLoc = new GameObject();
        targetLoc.AddComponent<SphereCollider>();
        targetLoc.GetComponent<SphereCollider>().isTrigger = true;
        targetLoc.AddComponent<TargetChecker>();
        targetLoc.transform.parent = transform;
        targetLoc.name = "Target Location";
        targetLoc.AddComponent<Rigidbody>();
        targetLoc.GetComponent<Rigidbody>().isKinematic = true;
        targetLoc.GetComponent<Rigidbody>().useGravity = false;
    }

    public override void Update()
    {
        base.Update();
        LevelUp();

        if (targetMode)
        {
            movement.canMove = false;
            targetLoc.transform.position = something() + new Vector3(0, 0.15f, 0);
            targetLoc.DrawCircle(SelectedSkill.Radius, 0.1f);
            targetLoc.GetComponent<SphereCollider>().radius = SelectedSkill.Radius;
            if (Input.GetButtonDown("Fire1"))
            {            
                UnitInformation[] GOs = targetLoc.GetComponent<TargetChecker>().UIs.ToArray();
                foreach(UnitInformation ui in GOs)
                {
                    ui.ModifyStat(Stats.CurrentHealth, -SelectedSkill.TargetDamage);
                    foreach(Status s in SelectedSkill.TargetStatuses)
                    {
                        ui.NewStatus(s);
                    }
                }

                ModifyStat(Stats.CurrentHealth, -SelectedSkill.PlayerDamage);
                foreach(Status s in SelectedSkill.PlayerStatuses)
                {
                    NewStatus(s);
                }
            }
        }
        else if (!targetMode && inCombat && myTurn)
        {
            movement.canMove = true;
        }
        else if (!targetMode && !inCombat)
        {
            movement.canMove = true;
        }

        if (!targetMode)
        {
            targetLoc.transform.position = transform.position;
            targetLoc.DrawCircle(0f, 0f);
        }
    }

    private Vector3 something() // hahhah need to name this properly (gets location of where the mouse is hovering)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            Vector3 location = hit.point;
            return location;
        }

        return transform.position;
    }

    private void LevelUp()
    {
        if (GetStat(Stats.Experience) >= xpNeeded())
        {
            ModifyStat(Stats.Experience, -xpNeeded()); //Remove the amount of XP needed to level up

            if (GetStat(Stats.Level) == 25)
            {
                ModifyStat(Stats.Paragon, 1); //Player is max level yet, increase paragon level
            }
            else
            {
                ModifyStat(Stats.Level, 1); //Player isnt max level yet, increase normal level
            }
        }
    }

    private float xpNeeded()
    {
        if (GetStat(Stats.Level) == 25)
        {
            return ParagonXP();
        }
        else
        {
            return LevelXP();
        }
    }

    private float LevelXP()
    {
        float xpNeeded = Mathf.Pow(1.25f, GetStat(Stats.Level)) * 100;
        return xpNeeded;
    }

    private float ParagonXP()
    {
        float xpNeeded = Mathf.Pow(1.06f, GetStat(Stats.Level)) * 100;
        return xpNeeded;
    }
}
