using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLighting : MonoBehaviour
{
    public GameObject Red, Blue;
    public bool Complete = false;

    // Start is called before the first frame update
    void Start()
    {
        Red = gameObject.transform.Find("Red").gameObject;
        Blue = gameObject.transform.Find("Blue").gameObject;
    }

    public void Change()
    {
        Complete = true;
        Red.SetActive(true);
        Blue.SetActive(false);
    }
}
