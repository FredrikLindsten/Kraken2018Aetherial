using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_crystal : MonoBehaviour {
    // Use this for initialization

    AudioSource audioSource;
    int temp;
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnDestroy()
    {
        scr_utilities.instance.aetherLeft += 10;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(-Time.deltaTime, 0, 0);


	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Harpoon")
        {
            audioSource.Play();
        }
    }
}
