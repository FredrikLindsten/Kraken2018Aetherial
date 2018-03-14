using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    AudioSource audioSource;

    public AudioClip menuTheme;
    public AudioClip tutorialTheme;
    public AudioClip scene2Theme;
    public AudioClip scene3Theme;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(audioSource.isPlaying == false)
        {
            playTheme();
        }
	}

    void playTheme()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.PlayOneShot(menuTheme);
        } else if (SceneManager.GetActiveScene().buildIndex == 1) {
            audioSource.PlayOneShot(tutorialTheme);
        } else if (SceneManager.GetActiveScene().buildIndex == 2) {
            audioSource.PlayOneShot(scene2Theme);
        } else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            audioSource.PlayOneShot(scene3Theme);
        }
    }
}
