using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour {

    NavMeshAgent nv;
    LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        nv = transform.parent.GetComponent<NavMeshAgent>();	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3[] corners = nv.path.corners;

        lr.positionCount = nv.path.corners.Length;
        lr.SetPositions(nv.path.corners);
	}
}
