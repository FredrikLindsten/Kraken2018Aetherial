using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_crystal : MonoBehaviour {
    // Use this for initialization

    AudioSource audioSource;
    
	void Start ()
    {
        transform.localScale = new Vector3(0.2f / transform.parent.localScale.x, 0.2f / transform.parent.localScale.y, 1);
        GetComponent<SpriteRenderer>().sprite = scr_cloudcontroller.instance.crystalArt;
        audioSource = GetComponent<AudioSource>();
	}

    private void OnDestroy()
    {
        scr_utilities.instance.aetherLeft += 10;
    }

    // Update is called once per frame
    void Update () {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Harpoon" && other.GetComponent<Harpoon>().isFired == true)
        {
            audioSource.Play();
        }
    }
}
