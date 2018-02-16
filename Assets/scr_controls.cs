using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_controls : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            GetComponentInParent<scr_startlevel>().LeaveControls();
	}
}
