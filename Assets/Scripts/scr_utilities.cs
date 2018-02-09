using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scr_utilities : MonoBehaviour {

    public static scr_utilities instance = null;
    public static GameObject player;
    public static GameObject leviathan;

    public static float[] edges = new float[4];
    public static float screenWidth, screenHeight;
    public enum edgeId { Left, Right, Bottom, Top};

    public Slider playerHealthUI;
    public Slider leviathanHealthUI;

    public static float padding = 2;

    public void Hide(float timer)
    {
        scr_skyslugMovement.visibility = false;
        StartCoroutine(LoseSight(timer));
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
        leviathan = GameObject.FindGameObjectWithTag("Boss");
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
        //TODO update aether bar
        playerHealthUI.value = player.GetComponent<scr_hpsystem>().getHealthPercent();
        leviathanHealthUI.value = leviathan.GetComponent<scr_hpsystem>().getHealthPercent();
    }
}
