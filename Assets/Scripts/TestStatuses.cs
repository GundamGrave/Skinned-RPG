using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatuses : MonoBehaviour
{

    public UnitInformation target;

    public List<TEST> t = new List<TEST>();


    [System.Serializable]
    public struct TEST
    {
        public Status status;
        public KeyCode key;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < t.Count; i++)
        {
            if (Input.GetKeyDown(t[i].key))
            {
                target.NewStatus(t[i].status);
            }
        }
    }
}
