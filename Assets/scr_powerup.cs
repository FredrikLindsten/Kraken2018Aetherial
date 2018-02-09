using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerup : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

    private void OnDestroy()
    {
        scr_utilities.instance.aetherLeft += 10;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(-Time.deltaTime, 0, 0);
	}
}
