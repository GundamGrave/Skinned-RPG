using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public UnitInformation[] go;

    public UnitInformation[] battleOrder;

    public int CurrentTurn;

    public bool InCombat;

    public int outsideCombatTimer = 5;
    bool timerRunning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InCombat)
        {
            if (timerRunning)
            {
                timerRunning = false;
                StopCoroutine("OutsideCombat");
                FindCombatants();
            }

            if (CurrentTurn == battleOrder.Length)
                CurrentTurn = 0;

            if (CurrentTurn == 0)
                UpdateSpeedList();

            battleOrder[CurrentTurn].myTurn = true;  
        }
        else
        {
            battleOrder = null;
            if (!timerRunning)
            {
                timerRunning = true;
                StartCoroutine("OutsideCombat");
            }
            return;
        }
    }

    private void FindCombatants()
    {
        go = FindObjectsOfType<UnitInformation>();
        foreach (UnitInformation u in go)
        {
            u.ApplyStatuses();
        }
    }

    private void UpdateSpeedList()
    {
        if (InCombat)
        {
            List<UnitInformation> fastList = new List<UnitInformation>();
            fastList.AddRange(go);

            fastList.Sort(delegate (UnitInformation a, UnitInformation b)
            {
                return -a.GetStat(UnitInformation.Stats.Initiative).CompareTo(b.GetStat(UnitInformation.Stats.Initiative));
            });

            battleOrder = fastList.ToArray();
        }

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
    }

    IEnumerator OutsideCombat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            outsideCombatTimer--;
            if (outsideCombatTimer == 0)
            {
                TurnManager.TurnEnded.Invoke();
                outsideCombatTimer = 5;
            }
        }
    }
}
