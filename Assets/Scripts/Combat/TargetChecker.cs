using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChecker : MonoBehaviour
{
    public SphereCollider sc;
    public List<UnitInformation> UIs = new List<UnitInformation>();
    private void Start()
    {
        sc = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<UnitInformation>() != null)
        {
            UIs.Add(other.gameObject.GetComponent<UnitInformation>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<UnitInformation>() != null)
        {
            UIs.Remove(other.gameObject.GetComponent<UnitInformation>());
        }
    }
}
