using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : UnitInformation
{
    public bool inCombat;
    public Movement movement;
    public GameObject EndTurn;

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
    }

    public override void Update()
    {
        base.Update();
        LevelUp();

        if (myTurn)
        {
            EndTurn.SetActive(true);
        }
        else
        {
            EndTurn.SetActive(false);
        }

        if(GetStat(Stats.CurrentHealth) <= 0)
        {
            SceneManager.LoadScene("Game Over");
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
                ModifyStat(Stats.Strength, 1);
                ModifyStat(Stats.Intelligence, 1);
                ModifyStat(Stats.Initiative, 1);
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
