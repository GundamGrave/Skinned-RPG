using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyStats : MonoBehaviour
{
    public SphereCollider sc;


    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Work Trigger Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Work Trigger Exit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Work Collision Enter");
    }
}
