using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "RPG/Skill")]
public class Skill : ScriptableObject
{
    [Header("Base Skill")]
    public int Cost;
    public int SkillCooldown;
    public int CooldownTimer = 0;
    public bool isAvailable = true;

    [Header("Target"), Space(10)]
    public float Radius;


    [Header("Target Effects"), Space(10)]
    public int TargetDamage;
    public Status[] TargetStatuses;
    public int TargetStatusDuration;

    [Header("Self Effects"), Space(10)]
    public int PlayerDamage;
    public Status[] PlayerStatuses;
    public int PlayerStatusDuration;

    public virtual void UseSkill()
    {
        isAvailable = false;
    }

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
}
