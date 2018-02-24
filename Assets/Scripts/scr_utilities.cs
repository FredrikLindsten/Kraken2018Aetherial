using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEngine.SceneManagement; //temp for beta


public class scr_utilities : MonoBehaviour {

    public static scr_utilities instance = null;
    public static GameObject player;
    public static GameObject leviathan;
    public static List<scr_skyslugMovement> slugs = new List<scr_skyslugMovement>();

    public static float[] edges = new float[4];
    public static float screenWidth, screenHeight;
    public enum edgeId { Left, Right, Bottom, Top};

    public int cloudCover = 0;
    public int islandCount = 0;

    public GameObject pauseOverlay;
    public Slider playerHealthUI;
    public Slider playerAetherUI;
    public Slider leviathanHealthUI;

    public GameObject victoryScreen;
    public GameObject deathScreen;

    public GameObject transitionEffect;

    public float aetherLeft = 0;
    private float aetherMax = 0;

    public static float padding = 2;

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        aetherMax = aetherLeft;
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        leviathan = GameObject.FindGameObjectWithTag("Boss");
        edges[0] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        edges[1] = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        edges[2] = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        edges[3] = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        screenWidth = edges[1] - edges[0];
        screenHeight = edges[3] - edges[2];
    }

    // Use this for initialization
    void Start () {
        scr_cloudcontroller.instance.SetCloudCover(cloudCover);
        scr_cloudcontroller.instance.SetIslandCount(islandCount);
	}

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            playerHealthUI.value = player.GetComponent<scr_hpsystem>().getHealthPercent();
        playerAetherUI.value = aetherLeft / aetherMax;
        leviathanHealthUI.value = leviathan.GetComponent<scr_hpsystem>().getHealthPercent();

        if(aetherLeft > aetherMax)
        {
             aetherLeft = aetherMax;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseOverlay.activeSelf)
            {
                pauseOverlay.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseOverlay.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void Victory()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 )
        {
            victoryScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(2); //temp for beta
        }
        
        
    }

    public void Death()
    {
        deathScreen.SetActive(true);
    }

    public void Checkpoint()
    {
        StartCoroutine(CheckpointWaiting());
    }

    IEnumerator CheckpointWaiting()
    {
        scr_stormcloud transition = Instantiate(transitionEffect, new Vector3(scr_utilities.screenWidth + (2 * scr_utilities.padding), 0, 1), Quaternion.identity).GetComponent<scr_stormcloud>();
        pauseOverlay.SetActive(true);
        while(true)
        {
            yield return null;
            if (Input.GetKeyUp(KeyCode.Space))
                break;
        }
        pauseOverlay.SetActive(false);
        transition.speed = scr_cloud.speed;
    }
}
