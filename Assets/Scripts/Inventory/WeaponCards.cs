using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCards", menuName = "RPG/WeaponCards")]
public class WeaponCards : ScriptableObject
{
    public GameObject gameObject;

    public string WeaponName = "";

    public int DamageMod;
    public int HPMod;
    public int MPMod;

    public Skill skill;
        
    public string FlavText = "";
}
