using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_utilities : MonoBehaviour {

    public static scr_utilities instance = null;
    public static GameObject player;

    public static float[] edges = new float[4];
    public static float screenWidth, screenHeight;
    public enum edgeId { Left, Right, Bottom, Top};

    public static float padding = 2;

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
        if(instance != null)
            Destroy(this);
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        edges[0] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        edges[1] = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        edges[2] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        edges[3] = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        screenHeight = edges[1] - edges[0];
        screenWidth = edges[3] - edges[2];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
