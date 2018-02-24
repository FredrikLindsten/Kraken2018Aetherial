using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betascript : MonoBehaviour {
    bool beta = true;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (beta == true)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<scr_hpsystem>().takeDamage(1);
            beta = false;
        }
        
        Debug.Log(GameObject.FindGameObjectWithTag("Boss").GetComponent<scr_hpsystem>().getHealth());
	}
}
