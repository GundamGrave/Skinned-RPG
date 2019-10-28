using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewRoom : MonoBehaviour
{
    public RoomInfo room;
    private GameObject tile2x2;
    private GameObject exit, enemy;
    public TorchLighting[] torches;
    float[] stats = new float[4];

    private List<GameObject> enemies = new List<GameObject>();

    Vector3 v3Exit;

    private void Start()
    {
        tile2x2 = (GameObject)Resources.Load("2x2");
        exit = (GameObject)Resources.Load("2x2Exit");

        WorldTracker.instance.numOfRooms++;
    }

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
                    if (i == 3)
                    {
                        GameObject tile = Instantiate(tile2x2);
                        tile.transform.parent = NewRoom.transform;
                        tile.transform.localPosition = new Vector3(2 * x, 0, 2 * z);

                        RandomData();
                        GameObject e = Instantiate(enemy);
                        //e.GetComponent<NavMeshAgent>().enabled = false;
                        e.transform.parent = NewRoom.transform;
                        e.transform.localPosition = new Vector3(2 * x, 1, 2 * z);
                        enemies.Add(e);
                    }
                    else if (i == 2)
                    {
                        GameObject tile = Instantiate(exit);
                        tile.transform.parent = NewRoom.transform;
                        tile.transform.localPosition = new Vector3(2 * x, 0, 2 * z);
                        v3Exit = new Vector3(2 * x, -30, 2 * z);
                        tile.GetComponentInChildren<ExitNewRoom>().WorldRoom = this;
                    }
                    else if (i == 1)
                    {
                        GameObject tile = Instantiate(tile2x2);
                        tile.transform.parent = NewRoom.transform;
                        tile.transform.localPosition = new Vector3(2 * x, 0, 2 * z);
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

    private void RandomData()
    {
        stats[0] = Random.Range(1, 5);
        stats[1] = Random.Range(stats[0] * 2, stats[0] * 4);
        stats[2] = Random.Range(stats[0] * 2, stats[0] * 4);
        stats[3] = Random.Range(stats[0] * 2, stats[0] * 4);

        int num = Random.Range(0, 3);
        switch (num)
        {
            case 0:
                enemy = (GameObject)Resources.Load("EnemyWarriorBlue");
                enemy.GetComponent<AIStats>().Sprite = Resources.Load<Sprite>("Sprites/EnemyBlue");
                break;

            case 1:
                enemy = (GameObject)Resources.Load("EnemyWarriorRed");
                enemy.GetComponent<AIStats>().Sprite = Resources.Load<Sprite>("Sprites/EnemyRed");
                break;

            case 2:
                enemy = (GameObject)Resources.Load("EnemyWarriorGreen");
                enemy.GetComponent<AIStats>().Sprite = Resources.Load<Sprite>("Sprites/EnemyGreen");
                break;

            default:
                enemy = (GameObject)Resources.Load("EnemyWarriorBlue");
                enemy.GetComponent<AIStats>().Sprite = Resources.Load<Sprite>("Sprites/EnemyBlue");
                break;
        }

        enemy.GetComponent<AIStats>().stats = stats;
    }
}
