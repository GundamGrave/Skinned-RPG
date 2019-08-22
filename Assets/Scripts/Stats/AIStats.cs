using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class AIStats : UnitInformation
{
    public SphereCollider sc;
    public NavMeshAgent navMesh;
    public PlayerStats player;


    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();

        sc = GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 10;

        player = FindObjectOfType<PlayerStats>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        base.Update();
        if (myTurn)
        {
            navMesh.destination = player.transform.position;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance <= 5)
            {
                navMesh.destination = transform.position;
                EndTurn();
            }
        }
    }
}
