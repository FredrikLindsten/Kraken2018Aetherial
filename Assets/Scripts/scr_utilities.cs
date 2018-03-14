﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEngine.SceneManagement; //temp for beta

public enum edgeId { Left, Right, Bottom, Top };

public class scr_utilities : MonoBehaviour {

    public static scr_utilities instance = null;
    public static GameObject player;
    public static GameObject leviathan;

    private static float[] edges = new float[4];
    public static float screenWidth, screenHeight;

    public int cloudCover = 0;
    public int islandCount = 0;

    public GameObject pauseOverlay;
    public GameObject checkpoint;
    public Slider playerHealthUI;
    public Slider playerAetherUI;
    public Slider leviathanHealthUI;

    public GameObject victoryScreen;
    public GameObject deathScreen;

    public GameObject transitionEffect;

    public Texture2D checkpointSprite;

    public float aetherLeft = 0;
    private float aetherMax = 0;
    private bool waitingAtCheckpoint = false;
    public void StopWaiting()
    {
        waitingAtCheckpoint = false;
    }

    public static float padding = 2;

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        aetherMax = aetherLeft;
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
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
        leviathan = scr_leviathan.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            playerHealthUI.value = player.GetComponent<scr_hpsystem>().getHealthPercent();
        playerAetherUI.value = aetherLeft / aetherMax;
        if (leviathan != null)
            leviathanHealthUI.value = leviathan.GetComponent<scr_hpsystem>().getHealthPercent() + 0.1f;

        if(aetherLeft > aetherMax)
        {
             aetherLeft = aetherMax;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseOverlay.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Victory()
    {
        if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
        {
            victoryScreen.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            for (int i = 0; i< GameObject.FindGameObjectsWithTag("Enemy").Length;i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Enemy")[i]);
            }
        }
        else
        {
            StartCoroutine(CheckpointWaiting());
        }
    }

    public void Death()
    {
        deathScreen.SetActive(true);
    }

    IEnumerator CheckpointWaiting()
    {   
        scr_stormcloud transition = Instantiate(transitionEffect, new Vector3(0, 0, 1), Quaternion.identity).GetComponent<scr_stormcloud>();
        scr_noise noise = transition.transform.GetComponentInChildren<scr_noise>();
        noise.GetComponent<Renderer>().material.SetTexture("_DetailAlbedoMap", checkpointSprite);
        scr_cloudcontroller.instance.SetIslandCount(0);
        scr_cloudcontroller.instance.SetCloudCover(0);
        DontDestroyOnLoad(transition);
        yield return new WaitForSeconds(5);
        checkpoint.SetActive(true);
        waitingAtCheckpoint = true;
        yield return new WaitForSeconds(8);
        checkpoint.SetActive(false);
        transition.wait = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static float GetEdge(edgeId val, bool padded)
    {
        float ret = edges[(int)val];
        if (padded)
            if (val == edgeId.Bottom || val == edgeId.Left)
                ret -= padding;
            else
                ret += padding;
        return ret;
    }
}
