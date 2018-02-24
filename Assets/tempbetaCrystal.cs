using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempbetaCrystal : MonoBehaviour {
    public Sprite spriteBlue;
	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "PlaytestScene")
        {
            Debug.Log("Yeeee");
            GetComponent<SpriteRenderer>().sprite = spriteBlue;
        }
    }
	
	// Update is called once per frame
	void Update () {

    }
}
