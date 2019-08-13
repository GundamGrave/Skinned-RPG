using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitInformation
{
    public int str, dex, intel, spd;

    public override void InitializeStats()
    {
        base.InitializeStats();

        SetStat(Stats.Strength, str);
        SetStat(Stats.Dexterity, dex);
        SetStat(Stats.Intelligence, intel);
        SetStat(Stats.Speed, spd);
    }

    public override void Start()
    {
        base.Start();
    }

    
}
