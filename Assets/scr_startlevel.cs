using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_startlevel : MonoBehaviour {

    static GameObject instance;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this.gameObject;
        else
            Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("PlaytestScene");
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
