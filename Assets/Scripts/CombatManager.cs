using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CombatManager : MonoBehaviour
{
    public List<UnitInformation> go = new List<UnitInformation>();

    public List<UnitInformation> battleOrder = new List<UnitInformation>();

    public PlayerStats Player;

    public GameObject CardCanvas;
    //public GameObject CombatCanvas;

    private UnitInformation CurrentUnit;

    public int CurrentTurn;
    public int CurrentRound = 1;

    public bool InCombat;

    public int outsideCombatTimer = 5;
    bool timerRunning = false;

    public Image[] Combatants;
    public int MaxNumberVis = 7;
    public bool Visible = false;

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
            CardCanvas.SetActive(true);
            //CombatCanvas.SetActive(true);

            if (battleOrder == null)
            {
                FindCombatants();
                UpdateSpeedList();
            } // Start of Combat

            if (!battleOrder[CurrentTurn].myTurn)
            {
                CurrentUnit = battleOrder[CurrentTurn];
                battleOrder[CurrentTurn].myTurn = true;
                battleOrder[CurrentTurn].ApplyStatuses();
            }           

            if (go.Count == 1)
            {
                InCombat = false;
                Player.gameObject.GetComponent<Movement>().canMove = true;
                Player.myTurn = false;
                CurrentRound = 0;
                CurrentTurn = 0;
                FindObjectOfType<ExitNewRoom>().Empty = true;
                ManageCombatOrderVisuals();
                return;
            } // Ending combat

            if (CurrentTurn == battleOrder.Count)
            {
                CurrentTurn = 0;
                CurrentRound++;
            }

            if (CurrentTurn == 0)
                UpdateSpeedList();

            ManageCombatOrderVisuals();

            if (timerRunning) // Start of Combat
            {
                timerRunning = false;
                StopCoroutine("OutsideCombat");
            }

            ManageTurnVisuals();
        }
        else
        {
            CardCanvas.SetActive(false);
            //CombatCanvas.SetActive(false);
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
        UpdateSpeedList();
        if (CurrentTurn >= battleOrder.Count)
        {
            CurrentTurn = 0;
            CurrentRound++;
        }
    }

    private void FindCombatants()
    {
        go.Clear();
        go.AddRange(FindObjectsOfType<UnitInformation>());
        foreach(UnitInformation ui in go)
        {
            ui.GetComponent<NavMeshAgent>().enabled = true;
            ui.GetComponent<NavMeshAgent>().isStopped = true;
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

    private void ManageTurnVisuals()
    {
        if (InCombat)
        {
            if (!Visible)
            {
                foreach (Image i in Combatants)
                {
                    i.gameObject.SetActive(true);
                }
                Visible = true;
            }

            if (Visible && battleOrder != null)
            {
                int counter = 0;
                while (counter < 7)
                {
                    foreach (UnitInformation ui in battleOrder)
                    {
                        Combatants[counter].sprite = ui.Sprite;
                        counter++;
                        if (counter == 7)
                            break;
                    }
                }
            }

            if (Visible)
            {
                //Get whose turn it is
                //Display them first, then everyone else after them
                int n = CurrentTurn;
                foreach (Image i in Combatants)
                {
                    i.sprite = battleOrder[n].Sprite;
                    n++;
                    if (n == battleOrder.Count)
                        n = 0;
                }
            }
        }

        if (!InCombat && Visible)
        {
            foreach (Image i in Combatants)
            {
                i.gameObject.SetActive(false);
            }
            Visible = false;
        }
    }

    public void StartCombat()
    {
        CurrentRound = 0;
        foreach (UnitInformation ui in go)
        {
            ui.SetStat(UnitInformation.Stats.ActionPoints, 4);
        }
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

    private void ManageCombatOrderVisuals()
    {
        if (InCombat)
        {
            if (!Visible)
            {
                foreach (Image i in Combatants)
                {
                    i.gameObject.SetActive(true);
                }
                Visible = true;
            }

            if (Visible && battleOrder != null)
            {
                int counter = 0;
                while (counter < 7)
                {
                    foreach (UnitInformation ui in battleOrder)
                    {
                        /*GameObject combatant = new GameObject();
                        combatant.AddComponent<Image>();
                        combatant.GetComponent<Image>().sprite = ui.Sprite;
                        combatant.transform.parent = gameObject.transform; 
                        int pos = (-125) * counter;
                        v = new Vector3(0, pos, 0);
                        combatant.GetComponent<RectTransform>().localPosition = v;
                        Combatants.Add(combatant);*/
                        Combatants[counter].sprite = ui.Sprite;
                        counter++;
                        if (counter == 7)
                            break;
                    }
                }
            }

            if (Visible)
            {
                //Get whose turn it is
                //Display them first, then everyone else after them
                int n = CurrentTurn;
                foreach (Image i in Combatants)
                {
                    i.sprite = battleOrder[n].Sprite;
                    n++;
                    if (n == battleOrder.Count)
                        n = 0;
                }
            }
        }

        if (!InCombat && Visible)
        {
            foreach (Image i in Combatants)
            {
                i.gameObject.SetActive(false);
            }
            Visible = false;
        }
    }
}
