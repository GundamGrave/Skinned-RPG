using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    //public Items[] inventory = new Items[10];
    public CompleteCard head, torso, legs, weapon;
    public UnitInformation ps;

    private int[,] mods = new int[4, 3];
    public int[] StatChanges = new int[3];

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if(head != null)
        {
            mods[0, 0] = head.strMod;
            mods[0, 1] = head.intMod;
            mods[0, 2] = head.spdMod;
        }
        else
        {
            mods[0, 0] = 0;
            mods[0, 1] = 0;
            mods[0, 2] = 0;
        }

        if (torso != null)
        {
            mods[1, 0] = torso.strMod;
            mods[1, 1] = torso.intMod;
            mods[1, 2] = torso.spdMod;
        }
        else
        {
            mods[1, 0] = 0;
            mods[1, 1] = 0;
            mods[1, 2] = 0;
        }

        if (legs != null)
        {
            mods[2, 0] = legs.strMod;
            mods[2, 1] = legs.intMod;
            mods[2, 2] = legs.spdMod;
        }
        else
        {
            mods[2, 0] = 0;
            mods[2, 1] = 0;
            mods[2, 2] = 0;
        }

        if (weapon != null)
        {
            mods[3, 0] = weapon.strMod;
            mods[3, 1] = weapon.intMod;
            mods[3, 2] = weapon.spdMod;
        }
        else
        {
            mods[3, 0] = 0;
            mods[3, 1] = 0;
            mods[3, 2] = 0;
        }

        StatChanges[0] = mods[0, 0] + mods[1, 0] + mods[2, 0] + mods[3, 0];
        StatChanges[1] = mods[0, 1] + mods[1, 1] + mods[2, 1] + mods[3, 1];
        StatChanges[2] = mods[0, 2] + mods[1, 2] + mods[2, 2] + mods[3, 2];

        ps.StatChanges = StatChanges;
    }
}
