using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CompleteCard", menuName = "RPG/CompleteCard")]
public class CompleteCard : ScriptableObject
{
    // Card basics

   public Sprite cardArt;

    public string cardName = "";
    public string flavText = "";

    // Card Classes

    public enum CardType
    {
        BasicAttackCard,
        WeaponCard,
        HeadArmourCard,
        TorsoArmourCard,
        LegsArmourCard,
        ItemCard,
        SpellCard,
    }

    public enum SetType
    {
        None,
        Type1,
        Type2,
        Type3,
    }

    public CardType cardType;

    public SetType setType;

    //Stat Modifiers

    public int strMod;
    public int intMod;
    public int spdMod;

    //Spell Cards Only

    public Skill Spell;
    public string GetDescription()
    {
        string desc = "";
        desc += Spell.GetDescription();
        return desc;
    }
       
}
