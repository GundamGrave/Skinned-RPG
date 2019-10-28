using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
    public UnitInformation unit;
    public UnitInformation.Stats stat;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = stat.ToString() + ": " + unit.GetStat(stat);
        text.text = unit.GetStat(stat).ToString("F0");
    }
}
