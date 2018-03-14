using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_weapon : MonoBehaviour {
    // Use this for initialization
    new public Transform transform;
    public GameObject beam;
    Quaternion targetRotation;
    public GameObject harpoon;
    public float rotationSpeed;

    private float rotationZone = 30;

    Rigidbody2D rb;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3 (GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y - 0.25f, GameObject.FindGameObjectWithTag("Player").transform.position.z) ;
        
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (Mathf.Sign(Mathf.DeltaAngle(rb.rotation,z))==1)
        {
            if (rb.rotation != z)
            {
                rb.MoveRotation(rb.rotation + rotationSpeed * Time.deltaTime);
            } else
            {
                return;
            }
            
        } else
        {
            if (rb.rotation != z)
            {
                rb.MoveRotation(rb.rotation + rotationSpeed * -Time.deltaTime);
            }
            else
            {
                return;
            }
        }
        if (Mathf.DeltaAngle(rb.rotation,z)< rotationZone && Mathf.DeltaAngle(rb.rotation, z)> - rotationZone && !GameObject.FindGameObjectWithTag("Beam"))
        {
            rb.rotation = z;
        }  else if (Mathf.DeltaAngle(rb.rotation, z) < rotationZone/10 && Mathf.DeltaAngle(rb.rotation, z) > -rotationZone/10)
        {
            rb.rotation = z;    
        }
        
        Debug.Log(Mathf.DeltaAngle(rb.rotation, z));
        /*transform.rotation = Quaternion.Euler(0f, 0f, z); */
        Debug.DrawRay(transform.position, transform.right);


        if (!GameObject.FindGameObjectWithTag("Beam"))
        {
            rotationSpeed = 500;
            //scr_utilities.instance.aetherLeft += 0.2f * Time.deltaTime; //passive aether regeneration
        } else
        {
            rotationSpeed = 50;
        }


    }
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameObject.FindGameObjectWithTag("Beam") && scr_utilities.instance.aetherLeft > 0)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().isFired == false)
            {
                GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().transform.rotation = Quaternion.Euler(0, 0, 0);
                GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().Fire();
            }
        }
    }

    void OnMouseButtonPressed()
    {

    }

    public Quaternion GetRigidBodyRotation()
    {
        return Quaternion.Euler(0, 0, rb.rotation);
    }

}
