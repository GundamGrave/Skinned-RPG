using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "RPG/Skill")]
public class Skill : ScriptableObject
{
    [Header("Base Skill")]
    public int Cost;
    //public int SkillCooldown;
    //public int CooldownTimer = 0;
    public bool isAvailable = true;

    [Header("Target"), Space(10)]
    public float Radius;
    public float Range;


    [Header("Target Effects"), Space(10)]
    public int TargetDamage;
    public Status[] TargetStatuses;

    [Header("Self Effects"), Space(10)]
    public int PlayerDamage;
    public Status[] PlayerStatuses;

    [Header("Visual FX")]
    public VisualFX CasterEffects;
    public VisualFX TargetEffects;


    public virtual void UseSkill()
    {
        isAvailable = false;
        if (CasterEffects != null)
        {
            // todo need a caster and target
            //CasterEffects.StartFX(transform);
        }
    }

    /*
    public void SkillTimer()
    {
        if (!isAvailable)
        {
            CooldownTimer += 1;
            if (CooldownTimer == SkillCooldown)
            {
                isAvailable = true;
                CooldownTimer = 0;
            }
        }
    }
    */

    public string GetDescription()
    {
        string desc = "";
        desc += "<b>" + name + "</b>";
        desc += "\n" + Cost.ToString() + " Energy";
        if (TargetDamage > 0)
        {
            desc += "\n" + TargetDamage.ToString() + " Damage";
        }
        if(PlayerDamage < 0)
        {
            desc += "\n" + (-PlayerDamage).ToString() + " Heal";
        }
        return desc;
    }
}
