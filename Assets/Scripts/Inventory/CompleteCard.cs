using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CompleteCard", menuName = "RPG/CompleteCard")]
public class CompleteCard : ScriptableObject
{
    // Card basics

    public GameObject cardborder;

    public Image cardArt;

    public string CardName = "";
    public string FlavText = "";

    // Card Classes

    public enum CardType
    {
        BasicAttackCard,
        WeaponCard,
        ArmourCard,
        ItemCard,
        SpellCard,
    }

    public CardType cardType;

    //Stat Modifiers

    public int HPMod;
    public int MPMod;

    //Spell Cards Only

    public Skill Spell;
    public int MPCost;
       
}
