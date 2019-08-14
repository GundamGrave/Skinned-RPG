using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmourCards", menuName = "RPG/ArmourCards")]
public class ArmourCards : ScriptableObject
{
    public GameObject gameObject;

    public string ArmourName = "";

    public int DamageMod;
    public int HPMod;
    public int MPMod;

    public Skill skill;

    public string FlavText = "";
}
