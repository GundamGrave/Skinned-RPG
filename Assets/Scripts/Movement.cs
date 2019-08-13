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

    [SerializeField] LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        //mask = LayerMask.GetMask("Tiles");
        navMesh = GetComponent<NavMeshAgent>();
        inCombat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !inCombat)
        {
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
