using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "RPG/Status")]
public class Status : ScriptableObject
{
    [Header("Base Status")]
    public int Duration;
    public int BaseDamage;

    [System.Serializable]
    public struct Modifier
    {
        public UnitInformation.Stats stat;
        public float delta;
    }

    [Header("Effects"), Space(10)]
    public Modifier[] modifiers;
    
}
