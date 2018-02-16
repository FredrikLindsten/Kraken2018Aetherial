using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_weapon : MonoBehaviour {
    // Use this for initialization
    new public Transform transform;
    public GameObject beam;
    Quaternion targetRotation;
    public GameObject harpoon;

    void Start () {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
        Debug.DrawRay(transform.position, transform.right);

        if (!GameObject.FindGameObjectWithTag("Beam"))
        {
            scr_utilities.instance.aetherLeft += 0.2f * Time.deltaTime; //passive aether regeneration
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(!GameObject.FindGameObjectWithTag("Beam") && scr_utilities.instance.aetherLeft > 0)
            {
                Instantiate(beam);
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(GameObject.FindGameObjectWithTag("Beam"));
        }
        if (!Input.GetMouseButton(0))
        {
            if (GameObject.FindGameObjectWithTag("Beam"))
            {
                Destroy(GameObject.FindGameObjectWithTag("Beam"));
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().transform.rotation = Quaternion.Euler(0, 0, -45);
            GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().Fire();
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().Fire();
        }
    }

    void OnMouseButtonPressed()
    {

    }
}
