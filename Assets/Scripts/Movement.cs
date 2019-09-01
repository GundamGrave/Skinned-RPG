using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public float MoveDistance;
    public bool inCombat;

    private Vector3 location;

    private NavMeshAgent navMesh;
    private PlayerStats ps;
    private CombatManager cm;

    public float distanceTravelled;

    private Vector3 lastPos;
    public bool canMove;


    [SerializeField] LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        cm = GameObject.Find("_gameManager").GetComponent<CombatManager>();
        //mask = LayerMask.GetMask("Tiles");
        navMesh = GetComponent<NavMeshAgent>();
        ps = GetComponent<PlayerStats>();
        inCombat = false;
    }

    // Update is called once per frame
    void Update()
    {
        inCombat = cm.InCombat;
        if(inCombat && !ps.myTurn)
        {
            navMesh.destination = transform.position;
        }

        if (canMove) //movement
        {
            if (EventSystem.current.IsPointerOverGameObject(-1) == false) // not over UI
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (inCombat && ps.myTurn)
                        ClickToMove();
                    else if (!inCombat)
                        ClickToMove();
                }
            } 
        }

        if (inCombat)
        {
            DistanceTravel();
        } // Distance tracker stuff
        else
        {
            lastPos = transform.position;
            distanceTravelled = 0;
        }
        if (!ps.myTurn)
        {
            distanceTravelled = 0;
        } // new turn, reset the tracker
    }

    private void ClickToMove()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (Vector3.Distance(transform.position, hit.point) <= MoveDistance * ps.GetStat(UnitInformation.Stats.ActionPoints) && inCombat) // Combat movement, limiting. Also probably not needed...
            {
                location = hit.point;
                navMesh.destination = location;
                navMesh.isStopped = false;
            }
            else if (!inCombat)
            {
                location = hit.point;
                navMesh.destination = location;
                navMesh.isStopped = false;
            }
        }
    }

    public void DistanceTravel()
    {
        distanceTravelled += Vector3.Distance(lastPos, transform.position);
        lastPos = transform.position;
        if(distanceTravelled >= MoveDistance)
        {
            distanceTravelled -= MoveDistance;
            ps.ModifyStat(UnitInformation.Stats.ActionPoints, -1);
        }
    }
}
