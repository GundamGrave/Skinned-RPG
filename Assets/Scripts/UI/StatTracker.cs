using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTracker : MonoBehaviour
{
    [SerializeField] Text[] text;
    [SerializeField] UnitInformation target;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        UpdateText();
    }


    void UpdateText()
    {
        text[0].text = "Max Health: " + target.GetStat(UnitInformation.Stats.MaxHealth);
        text[1].text = "Strength: " + target.GetStat(UnitInformation.Stats.Strength);
        text[2].text = "Dexterity: " + target.GetStat(UnitInformation.Stats.Dexterity);
        text[3].text = "Intelligence: " + target.GetStat(UnitInformation.Stats.Intelligence);
        text[4].text = "Speed: " + target.GetStat(UnitInformation.Stats.Speed);
    }

    
}
