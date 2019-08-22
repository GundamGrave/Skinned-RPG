using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitInformation
{
    public int str, intel, init;

    public bool inCombat;

    public override void InitializeStats()
    {
        base.InitializeStats();

        SetStat(Stats.Strength, str);
        SetStat(Stats.Intelligence, intel);
        SetStat(Stats.Initiative, init);
        SetStat(Stats.Level, 1);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        LevelUp();
    }

    private void LevelUp()
    {
        if(GetStat(Stats.Experience) >= xpNeeded())
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
        if(GetStat(Stats.Level) == 25)
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
