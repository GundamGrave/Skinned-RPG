using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<UnitInformation> go = new List<UnitInformation>();

    public List<UnitInformation> battleOrder = new List<UnitInformation>();

    public PlayerStats Player;

    public int CurrentTurn;
    public int CurrentRound = 1;

    public bool InCombat;

    public int outsideCombatTimer = 5;
    bool timerRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InCombat)
        {
            if(CurrentRound == 0 && CurrentTurn == 0)
            {
                foreach(UnitInformation ui in go)
                {
                    ui.SetStat(UnitInformation.Stats.ActionPoints, 4);
                }
            }

            if (go.Count == 1)
            {
                InCombat = false;
                Player.gameObject.GetComponent<Movement>().canMove = true;
                CurrentRound = 1;
                return;
            }

            if (battleOrder == null)
            {
                FindCombatants();
                UpdateSpeedList();
            }

            if (timerRunning)
            {
                timerRunning = false;
                StopCoroutine("OutsideCombat");             
            }           

            if (CurrentTurn == battleOrder.Count)
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

    public void RemoveCombatant(UnitInformation ui)
    {
        go.Remove(ui);
    }

    private void FindCombatants()
    {
        go.Clear();
        go.AddRange(FindObjectsOfType<UnitInformation>());
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

            battleOrder = fastList;
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
