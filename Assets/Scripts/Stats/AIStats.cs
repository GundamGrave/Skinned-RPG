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

    public float[] stats = new float[4];

    [SerializeField] float ShortestSkillDistance = Mathf.Infinity;

    

    public override void InitializeStats()
    {
        SetStat(Stats.ActionPoints, 4);
        SetStat(Stats.MaxHealth, 100);
        SetStat(Stats.CurrentHealth, 100);

        SetStat(Stats.Strength, stats[1]);
        SetStat(Stats.Intelligence, stats[2]);
        SetStat(Stats.Initiative, stats[3]);
        SetStat(Stats.Level, stats[0]);
    }

    public override void Start()
    {
        base.Start();
        
        foreach (Skill s in CurrentSkills)
        {
            if ((s.Radius/2 + s.Range) < ShortestSkillDistance)
            {
                ShortestSkillDistance = (s.Radius/2 + s.Range);
            }
        }

        lastPos = transform.position;
        sc = GetComponentInChildren<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 5;

        player = FindObjectOfType<PlayerStats>();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.enabled = true;
    }

    public override void Update()
    {
        base.Update();
        AP = GetStat(Stats.ActionPoints);
        if (GetStat(Stats.CurrentHealth) <= 0)
        {
            CM.RemoveCombatant(this);
            player.ModifyStat(Stats.Experience, 100);
            Destroy(gameObject);
            // Drop item
        }

        if (myTurn) //Do combat
        {
            navMesh.isStopped = false;
            DistanceTravel();
            float distance = Vector3.Distance(transform.position, player.transform.position);
            // If enough to use skill, get within distance to use said skill
            if (distance >= ShortestSkillDistance) // get within distance
            {
                navMesh.destination = player.transform.position;
            }
            else if (distance <= ShortestSkillDistance) // within distance, use skills
            {
                navMesh.isStopped = true; // make sure not to move
                if(AP >= 3) // use spell
                {
                    print(gameObject.name + " :USED SPELL ATTACK");
                    ModifyStat(Stats.ActionPoints, -3);
                    player.ModifyStat(Stats.CurrentHealth, -CurrentSkills[1].TargetDamage);
                    foreach(Status s in CurrentSkills[1].TargetStatuses)
                    {
                        player.NewStatus(s);
                    }
                }
                else if (AP == 2) // use basic attack
                {
                    print(gameObject.name + ": USED BASIC ATTACK");
                    ModifyStat(Stats.ActionPoints, -2);
                    player.ModifyStat(Stats.CurrentHealth, -CurrentSkills[0].TargetDamage);
                    foreach (Status s in CurrentSkills[0].TargetStatuses)
                    {
                        player.NewStatus(s);
                    }
                }
                else
                {
                    EndTurn();
                }
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
        if (other.gameObject.tag == "Player" && !CM.InCombat)
        {
            CM.InCombat = true;
            CM.StartCombat();
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
