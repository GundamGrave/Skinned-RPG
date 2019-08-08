using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTester : MonoBehaviour {

    [System.Serializable]
    public struct VFXTest
    {
        public VisualFX fx;
        public KeyCode key;
        public float duration;
        public VisualFX instance;
    }

    public VFXTest[] tests;

    VisualFXHolder holder;

	// Use this for initialization
	void Start () {
        holder = GetComponent<VisualFXHolder>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i= 0; i< tests.Length; i++)
        {
            if (Input.GetKeyDown(tests[i].key))
            {
                if (tests[i].duration == 0)
                {
                    // duration of 0 indicates a toggle, where the key turns it on and off
                    if (tests[i].instance != null)
                    {
                        tests[i].instance.StopFX();
                        tests[i].instance = null;
                    }
                    else
                        tests[i].instance = holder.AddFX(tests[i].fx, 0);
                }
                else
                    holder.AddFX(tests[i].fx, tests[i].duration);
            }
        }
	}
}
