using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public UnitInformation[] go;

    public UnitInformation[] battleOrder;

    public bool InCombat;

    // Start is called before the first frame update
    void Start()
    {
      go = FindObjectsOfType<UnitInformation>();
        foreach (UnitInformation u in go)
        {
            u.ApplyStatuses();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InCombat)
        {
            UpdateSpeedList();
        }
        else
        {
            battleOrder = null;
            return;
        }
    }

    private void UpdateSpeedList()
    {
        List<UnitInformation> fastList = new List<UnitInformation>();
        fastList.AddRange(go);

        fastList.Sort(delegate (UnitInformation a, UnitInformation b)
        {
            return a.GetStat(UnitInformation.Stats.Speed).CompareTo(b.GetStat(UnitInformation.Stats.Speed));
        });

        //UnitInformation nextFastest = null;
        //List<UnitInformation> fastList = new List<UnitInformation>();
        //List<UnitInformation> speedList = new List<UnitInformation>();

        //foreach (UnitInformation u in go)
        //{
        //    fastList.Add(u);
        //}

        //for (int i = 0; i < go.Length; i++)
        //{
        //    foreach (UnitInformation ui in fastList)
        //    {
        //        if (nextFastest == null)
        //        {
        //            nextFastest = ui;
        //        }
        //        else
        //        {
        //            if (nextFastest.GetStat(UnitInformation.Stats.Speed) < ui.GetStat(UnitInformation.Stats.Speed))
        //            {
        //                nextFastest = ui;
        //            }
        //        }
        //    }
        //    fastList.Remove(nextFastest);
        //    speedList.Add(nextFastest);
        //    nextFastest = null;
        //}

        battleOrder = fastList.ToArray();
    }
}
