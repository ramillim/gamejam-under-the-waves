using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float nTimerStart = 0;
    public float nTimerThreshold = 1.1f;
	// Use this for initialization
	void Start () {
        nTimerStart = Time.time;
        //nTimerThreshold = 1.1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - nTimerStart >= nTimerThreshold)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
    
}
