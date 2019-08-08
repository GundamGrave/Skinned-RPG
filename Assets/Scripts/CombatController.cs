using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitInformation))]
public class CombatController : MonoBehaviour
{
    // Checks action points and controls combat skills etc

    private UnitInformation unitInfo;
    private int maxAP;
    private int currentAP;
    private bool inCombat;
    
    private void Start()
    {
        unitInfo = GetComponent<UnitInformation>();

    }
}
