using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatuses : MonoBehaviour
{

    public UnitInformation target;

    public List<Status> Statuses = new List<Status>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            target.NewStatus(Statuses[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            target.NewStatus(Statuses[1]);
        }
    }
}
