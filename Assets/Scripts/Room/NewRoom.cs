﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewRoom : MonoBehaviour
{
    public RoomInfo room;
    public GameObject tile2x2;
    public GameObject exit;

    Vector3 v3Exit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject NewRoom = new GameObject();
            NewRoom.AddComponent<RoomTag>();
            NewRoom.GetComponent<RoomTag>().OrigPos = other.transform.position;
            int x = 0;
            
            foreach (RoomInfo.rowData rd in room.rows)
            {
                int z = 0;
                foreach (int i in rd.row)
                {
                    if (i == 1)
                    {
                        GameObject tile = Instantiate(tile2x2);
                        tile.transform.parent = NewRoom.transform;
                        tile.transform.localPosition = new Vector3(2 * x, 0, 2 * z);
                    }
                    else if (i == 2)
                    {
                        GameObject tile = Instantiate(exit);
                        tile.transform.parent = NewRoom.transform;
                        tile.transform.localPosition = new Vector3(2 * x, 0, 2 * z);
                        v3Exit = new Vector3(2 * x, -30, 2 * z);
                    }
                    z++;
                }
                x++;
            }

            NewRoom.transform.position = new Vector3(0, -30, 0);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.transform.position = v3Exit;
            other.GetComponent<NavMeshAgent>().enabled = true;

            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}