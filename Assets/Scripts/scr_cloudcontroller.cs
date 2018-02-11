using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloudcontroller : MonoBehaviour {

    public int cloudcover = 0;
    public GameObject cloudRef;
    public static scr_cloudcontroller instance;

    private List<GameObject> clouds = new List<GameObject>();

    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnClouds());
    }
	
	// Update is called once per frame
	void Update () {

    }

    void SetCloudCover(int val)
    {
        cloudcover = val;
    }

    void ReevaluateCloudCover()
    {
    }

    GameObject spawnCloud()
    {
        return Instantiate(cloudRef, Vector3.zero, Quaternion.identity);
    }

    IEnumerator SpawnClouds()
    {
        while (true) {
            if(clouds.Count < cloudcover)
            {
                //spawn clouds
                clouds.Add(spawnCloud());
            }
            while(clouds.Count > cloudcover)
            {
                //mark clouds for destruction
                int pickedCloud = Random.Range(0, clouds.Count - 1);
                clouds[pickedCloud].GetComponent<scr_cloud>().MarkForRemoval();
                clouds.RemoveAt(pickedCloud);
            }
            yield return new WaitForSeconds((scr_utilities.screenWidth + (2*scr_utilities.padding)) / cloudcover);
        }
    }
}
