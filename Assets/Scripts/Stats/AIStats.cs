using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStats : UnitInformation
{
    public SphereCollider sc;
    public NavMeshAgent navMesh;
    public PlayerStats player;

    public float MoveDistance;
    private Vector3 lastPos;
    private float distanceTravelled;

    public float AP;

    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        lastPos = transform.position;
        sc = GetComponentInChildren<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 5;

        player = FindObjectOfType<PlayerStats>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void Update()
    {
        if(GetStat(Stats.CurrentHealth) <= 0)
        {
            player.ModifyStat(Stats.Experience, 100);
            Destroy(gameObject);
        }

        AP = GetStat(Stats.ActionPoints);
        base.Update();
        if (myTurn)
        {
            navMesh.destination = player.transform.position;
            float distance = Vector3.Distance(transform.position, player.transform.position);
            DistanceTravel();
            if(distance <= 5)
            {
                navMesh.destination = transform.position;
                EndTurn();
            }
        }
    }

    public override void EndTurn()
    {
        base.EndTurn();
        navMesh.destination = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CM.InCombat = true;
        }
    }

    public void DistanceTravel()
    {
        distanceTravelled += Vector3.Distance(lastPos, transform.position);
        lastPos = transform.position;
        if (distanceTravelled >= MoveDistance)
        {
            distanceTravelled -= MoveDistance;
            ModifyStat(Stats.ActionPoints, -1);
        }
    }
}
