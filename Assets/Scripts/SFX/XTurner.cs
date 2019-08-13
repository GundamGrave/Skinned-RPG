using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTurner : MonoBehaviour
{
    public float speed = 90;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, Time.time * speed, 0);
    }
}

