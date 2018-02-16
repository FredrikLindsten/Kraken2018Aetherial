using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<LineRenderer>().SetPosition(0, GameObject.FindGameObjectWithTag("Harpoon").transform.position);
        GetComponent<LineRenderer>().SetPosition(1, GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
