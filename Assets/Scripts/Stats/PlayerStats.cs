using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitInformation
{


    public override void InitializeStats()
    {
        base.InitializeStats();

        SetStat(Stats.Strength, 10);
        SetStat(Stats.Dexterity, 20);
        SetStat(Stats.Intelligence, 30);
        SetStat(Stats.Speed, 40);
    }

    public override void Start()
    {
        base.Start();
    }

    
}
