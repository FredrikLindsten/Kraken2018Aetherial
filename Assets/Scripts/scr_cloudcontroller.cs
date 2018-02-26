using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloudcontroller : MonoBehaviour {

    public float speed = 0;
    private int cloudCount = 0;
    private int islandCount = 0;
    public GameObject islandRef;
    public GameObject cloudRef;

    public Sprite islandArt;
    public static scr_cloudcontroller instance = null;

    private List<GameObject> cloudsBg = new List<GameObject>();
    private List<GameObject> islands = new List<GameObject>();

    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public void SetCloudCover(int val)
    {
        int diff = val - cloudCount;
        cloudCount = val;
        for (int i = 0; i < diff; ++i)
        {
            //spawn clouds
            float distance = (scr_utilities.screenWidth + (2 * scr_utilities.padding));
            cloudsBg.Add(Instantiate(cloudRef, new Vector3(distance * (1 + (float)i / diff) - scr_utilities.edges[(int)scr_utilities.edgeId.Right], 0), Quaternion.identity));
        }
        while (cloudsBg.Count > cloudCount)
        {
            //mark clouds for destruction
            int pickedCloud = Random.Range(0, cloudsBg.Count - 1);
            cloudsBg[pickedCloud].GetComponent<scr_cloud>().MarkForRemoval();
            cloudsBg.RemoveAt(pickedCloud);
        }
    }

    public void SetIslandCount(int val)
    {
        int diff = val - islandCount;
        islandCount = val;
        for (int i = 0; i < diff; ++i)
        {
            //spawn clouds
            float distance = (scr_utilities.screenWidth + (2 * scr_utilities.padding));
            islands.Add(Instantiate(islandRef, new Vector3(distance * (1 + (float)i / diff) - scr_utilities.edges[(int)scr_utilities.edgeId.Right], 0), Quaternion.identity));
        }
        while (islands.Count > islandCount)
        {
            //mark clouds for destruction
            int pickedIsland = Random.Range(0, islands.Count - 1);
            islands[pickedIsland].GetComponent<scr_cloud>().MarkForRemoval();
            islands.RemoveAt(pickedIsland);
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
