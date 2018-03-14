using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_intromovie : MonoBehaviour {

    VideoPlayer videoPlayer;

    public GameObject Canvas;
    public GameObject Background;

    private void Awake()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        videoPlayer = GetComponent<VideoPlayer>();
        Canvas.SetActive(false);
        Background.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if ((long)videoPlayer.clip.frameCount == videoPlayer.frame)
        {
            gameObject.SetActive(false);
            Canvas.SetActive(true);
            Background.SetActive(true);
        }
	}
}
