using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitInformation : MonoBehaviour
{
    public List<Skill> CurrentSkills;
    public List<Status> CurrentStatuses = new List<Status>();

    public Dictionary<string, float> StatsDict = new Dictionary<string, float>();

    public static UnityEvent StatsChanged = new UnityEvent();

    public enum Stats
    {
        MaxHealth,
        CurrentHealth,
        Strength,
        Dexterity,
        Intelligence,
        Speed,
    };

    public virtual void Start()
    {
        InitializeStats();
        LoadSkills();
    }

    public virtual void InitializeStats()
    {
        SetStat(Stats.MaxHealth, 100);
        SetStat(Stats.CurrentHealth, 100);
    }

    // safely query the dictionary for a stat that may or may not exist
    float GetStat(string statName)
    {
        if (StatsDict.ContainsKey(statName))
            return StatsDict[statName];

        return 0;
    }

    public float GetStat(Stats stat)
    {
        return GetStat(stat.ToString());
    }

    public void SetStat(Stats stat, float value)
    {
        StatsDict[stat.ToString()] = value;
    }

    public float maxHealth
    {
        get
        {
            return GetStat(Stats.MaxHealth);
        }
        set
        {
            StatsDict[Stats.MaxHealth.ToString()] = value;
        }
    }

    private void LoadSkills()
    {
        //clone all skills so we have our own private copy
        if (CurrentSkills != null)
        {
            Skill[] skillArr = CurrentSkills.ToArray();
            for (int i = 0; i < skillArr.Length; i++)
            {
                if (skillArr[i] != null)
                    skillArr[i] = Instantiate(skillArr[i]);
            }
        }
        // register with the turn ended event
        TurnManager.TurnEnded.AddListener(AllSkillTimers);
    }

    private void AllSkillTimers()
    {
        foreach (Skill s in CurrentSkills)
            s.SkillTimer();
    }

    public void NewSkill(Skill skill)
    {
        Instantiate(skill);
        CurrentSkills.Add(skill);
    }

    public void NewStatus(Status status)
    {
        Instantiate(status);
        CurrentStatuses.Add(status);
    }

    void ApplyStatuses()
    {
        foreach(Status s in CurrentStatuses)
        {
            //find original stat?
        }
    }
}
