using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExitNewRoom : MonoBehaviour
{
    public bool Empty = false;
    public Vector3 OrigPos;
    public GameObject Room;
    public NewRoom WorldRoom;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Empty)
            {
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.transform.position = OrigPos;
                other.GetComponent<NavMeshAgent>().enabled = true;

                WorldTracker.instance.completedRooms++;
                foreach(TorchLighting tl in WorldRoom.torches)
                {
                    tl.Change();
                }
                Destroy(Room);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Room = FindObjectOfType<RoomTag>().gameObject;
        OrigPos = Room.GetComponent<RoomTag>().OrigPos;

        UnitInformation[] ui = FindObjectsOfType<UnitInformation>();
        if (ui.Length == 1)
        {
            //Empty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
