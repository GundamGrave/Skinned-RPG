using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFXHolder : MonoBehaviour {

    List<VisualFX> fxs = new List<VisualFX>();
    List<VisualFX> deathRow = new List<VisualFX>();

    public VisualFX AddFX(VisualFX fx, float duration)
    {
        VisualFX inst = Instantiate(fx);
        inst.duration = duration;
        inst.StartFX(transform);
        fxs.Add(inst);
        return inst;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // update all fx, storing those that have expired
        deathRow.Clear();
        foreach (VisualFX fx in fxs)
        {
            if (fx.UpdateFX(Time.deltaTime))
                deathRow.Add(fx);
        }

        // remove expired ones from list
        foreach (VisualFX fx in deathRow)
            fxs.Remove(fx);
	}
}
