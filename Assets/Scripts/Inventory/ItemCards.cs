using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCards", menuName = "RPG/ItemCards")]
public class ItemCards : ScriptableObject
{
    public GameObject gameObject;
    
    public string ItemName = "";

    public int DamageMod;
    public int HPMod;
    public int MPMod;

    public string FlavText = "";
}
