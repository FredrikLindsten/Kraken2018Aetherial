using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_intromovie : MonoBehaviour {

    VideoPlayer videoPlayer;
    scr_intromovie instance;

    public GameObject Canvas;
    public GameObject Background;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        videoPlayer = GetComponent<VideoPlayer>();
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
