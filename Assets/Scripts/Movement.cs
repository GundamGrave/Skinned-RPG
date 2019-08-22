using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public float MoveDistance;
    public bool inCombat;

    private Vector3 location;

    private NavMeshAgent navMesh;
    private PlayerStats ps;
    private CombatManager cm;

    [SerializeField] LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetButtonDown("Fire1"))
        {
            if (inCombat && ps.myTurn)
                ClickToMove();
            else if (!inCombat)
                ClickToMove();
        }
    }

    private void ClickToMove()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (Vector3.Distance(transform.position, hit.point) <= MoveDistance)
            {
                location = hit.point;
                navMesh.destination = location;
                navMesh.isStopped = false;
            }
        }
    }
}
