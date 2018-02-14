using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloudcontroller : MonoBehaviour {

    public float speed = 0;
    private int cloudcoverBg = 0;
    public GameObject cloudRef;
    public static scr_cloudcontroller instance = null;

    private List<GameObject> cloudsBg = new List<GameObject>();

    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public void SetCloudCover(int val)
    {
        int diff = val - cloudcoverBg;
        cloudcoverBg = val;
        for (int i = 0; i < diff; ++i)
        {
            //spawn clouds
            float distance = (scr_utilities.screenWidth + (2 * scr_utilities.padding));
            cloudsBg.Add(Instantiate(cloudRef, new Vector3(distance * (1 + (float)i / diff) - scr_utilities.edges[(int)scr_utilities.edgeId.Right], 0), Quaternion.identity));
        }
        while (cloudsBg.Count > cloudcoverBg)
        {
            //mark clouds for destruction
            int pickedCloud = Random.Range(0, cloudsBg.Count - 1);
            cloudsBg[pickedCloud].GetComponent<scr_cloud>().MarkForRemoval();
            cloudsBg.RemoveAt(pickedCloud);
        }
    }

    // Use this for initialization
    void Start () {
        scr_cloud.speed = speed;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
