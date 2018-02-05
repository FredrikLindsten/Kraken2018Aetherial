using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_utilities : MonoBehaviour {

    public static scr_utilities instance;
    public static GameObject player;

    public void Hide(float timer)
    {
        if (scr_skyslugMovement.visibility)
        {
            //remove powerup here
            scr_skyslugMovement.visibility = false;
            StartCoroutine(LoseSight(timer));
        }
    }

    IEnumerator LoseSight(float timer)
    {
        yield return new WaitForSeconds(timer);
        scr_skyslugMovement.visibility = true;
    }

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
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
