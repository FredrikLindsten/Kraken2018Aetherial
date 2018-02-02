using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_utilities : MonoBehaviour {

    private void Awake()
    {
        scr_spawner.edges[0] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        scr_spawner.edges[1] = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        scr_spawner.edges[2] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        scr_spawner.edges[3] = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
