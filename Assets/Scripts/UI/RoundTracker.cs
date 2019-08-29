using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTracker : MonoBehaviour
{
    private CombatManager CM;
    public Image[] Combatants;

    public int MaxNumberVis = 7;

    public bool Visible = false;

    // Start is called before the first frame update
    void Start()
    {
        CM = FindObjectOfType<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CM.InCombat)
        {
            if (!Visible && CM.battleOrder != null)
            {
                int counter = 0;
                while (counter < 7)
                {
                    foreach (UnitInformation ui in CM.battleOrder)
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
                Visible = true;
            }

            if (Visible)
            {
                //Get whose turn it is
                //Display them first, then everyone else after them
                int n = CM.CurrentTurn;
                foreach(Image i in Combatants)
                {
                    i.sprite = CM.battleOrder[n].Sprite;
                    n++;
                    if (n == CM.battleOrder.Count)
                        n = 0;
                }
            }
        }

        if(!CM.InCombat && Visible)
        {
            foreach(Image i in Combatants)
            {
                i.gameObject.SetActive(false);
            }
        }
    }
}
