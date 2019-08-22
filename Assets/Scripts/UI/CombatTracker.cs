using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatTracker : MonoBehaviour
{

    public int NumberOfActionPoints;

    public RawImage[] actionPoints;
    public Material[] materials;

    [SerializeField] UnitInformation player;
    // Start is called before the first frame update
    void Start()
    {
        //Find selected Unit

        //Show how many action points they have
        //NumberOfActionPoints = player.ActionPoints;

    }

    // Update is called once per frame
    void Update()
    {
        NumberOfActionPoints = 0;//player.ActionPoints;
        int counter = 0;
        foreach(RawImage raw in actionPoints)
        {
            if(counter < NumberOfActionPoints)
            {
                raw.material = materials[0];
                counter++;
            }
            else
            {
                raw.material = materials[1];
            }
        }
    }

    public void EndTurn()
    {
        player.EndTurn();
    }
}
