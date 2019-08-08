using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinter : MonoBehaviour {

    public Color tint;

    Renderer rend;
    Color original;

    public bool albedo;
    public bool emission;

    public float pulseSpeed = 0;
    public float pulseMin = 0.5f;
    public float pulseMax = 0.8f;
    [HideInInspector]
    public float pulseTimer;

    // Use this for initialization
    void Start () {
        Transform t = transform.parent;
        while (rend == null && t != null)
        {
            rend = t.GetComponent<Renderer>();
            t = t.parent;
        }

        if (rend) 
            original = rend.material.color;

        if (rend)
        {
            if (emission)
            {
                rend.material.EnableKeyword("_EMISSION");
                rend.material.SetFloat("_EmissionScaleUI", tint.a);
            }
            if (albedo)
                rend.material.color = tint;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float alpha = 1.0f;

        if (pulseSpeed > 0)
        {
            pulseTimer += Time.deltaTime * pulseSpeed;
            alpha *= pulseMin + (pulseMax - pulseMin) * Mathf.PingPong(pulseTimer, 1.0f);
        }

        Color col = tint;
        col.a *= alpha;

        if (rend)
        {
            Color col0 = col;
            if (albedo)
            {
                col0.r = original.r * (1 - alpha) + col.r * alpha;
                col0.g = original.g * (1 - alpha) + col.g * alpha;
                col0.b = original.b * (1 - alpha) + col.b * alpha;
                col0.a = 1;
                rend.material.color = col0;
            }
            if (emission)
            {
                col0 = col * Mathf.LinearToGammaSpace(col.a);
                rend.material.SetColor("_EmissionColor", col0);
            }
        }
    }

    void OnDestroy()
    {
        rend.material.color = original;
    }
}
