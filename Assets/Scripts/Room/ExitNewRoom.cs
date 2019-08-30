using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExitNewRoom : MonoBehaviour
{
    public bool Empty = false;
    public Vector3 OrigPos;
    public GameObject Room;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Empty)
            {
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.transform.position = OrigPos;
                other.GetComponent<NavMeshAgent>().enabled = true;

                Destroy(Room);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Room = FindObjectOfType<RoomTag>().gameObject;
        OrigPos = Room.GetComponent<RoomTag>().OrigPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
