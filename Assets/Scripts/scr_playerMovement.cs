using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerMovement : MonoBehaviour {
    KeyCode UpKey = KeyCode.W;
    KeyCode DownKey = KeyCode.S;
    KeyCode RightKey = KeyCode.D;
    KeyCode LeftKey = KeyCode.A;

    KeyCode TurnUp = KeyCode.R;
    KeyCode TurnDown = KeyCode.F;

    private Rigidbody2D rb;
    public float shipSpeed;
    public float inertia;
    private float thrustX;
    private float thrustY;
    private float velX;
    private float velY;
    public float maxThrust;
    private const float standStill = 0;

    new Transform transform;

    public float turnSpeed;
    private float turnZ;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    private void OnDestroy()
    {
        scr_utilities.player = scr_utilities.leviathan;
        scr_utilities.instance.Death();
        Destroy(GameObject.FindGameObjectWithTag("Harpoon"));
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey(UpKey) && thrustY < maxThrust) {
            thrustY += shipSpeed;
        } else if(!Input.GetKey(UpKey) && thrustY > standStill) {
            if ((thrustY -= inertia) <= 0)
            {
                thrustY = 0;
            }
        }
        if (Input.GetKey(DownKey) && thrustY > -maxThrust)
        {
            thrustY -= shipSpeed;
        }
        else if (!Input.GetKey(DownKey) && thrustY < standStill)
        {
            if ((thrustY += inertia) >= 0)
            {
                thrustY = 0;
            }
        }
        if (Input.GetKey(RightKey) && thrustX < maxThrust)
        {
            thrustX += shipSpeed;
        }
        else if (!Input.GetKey(RightKey) && thrustX > standStill)
        {
            if ((thrustX -= inertia) <= 0)
            {
                thrustX = 0;
            }
            
        }
        if (Input.GetKey(LeftKey) && thrustX > -maxThrust)
        {
            thrustX -= shipSpeed;
        }
        else if (!Input.GetKey(LeftKey) && thrustX < standStill)
        {
            if ((thrustX += inertia) >= 0)
            {
                thrustX = 0;
            }
            
        }
        velX = thrustX;
        velY = thrustY;
        rb.velocity = new Vector2 (velX, velY);

        if (Input.GetKey(TurnUp) && turnZ <= 45)
        {
            turnZ += turnSpeed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, turnZ));
        }
        else if (turnZ >= 1)
        {
            turnZ -= turnSpeed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, turnZ));
            if (turnZ <= 2 && turnZ >= 1)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
        if (Input.GetKey(TurnDown) && turnZ >= -45) 
        {
            turnZ -= turnSpeed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, turnZ));
        } else if (turnZ <= -1)
        {
            turnZ += turnSpeed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, turnZ));
            if (turnZ >= -2 && turnZ <= -1)
            {

            }
        } 


    }

    private void Update()
    {
        if (transform.position.x < scr_utilities.GetEdge(edgeId.Left,false))
            transform.position = new Vector3(scr_utilities.GetEdge(edgeId.Left, false), transform.position.y);
        if (transform.position.x > scr_utilities.GetEdge(edgeId.Right, false))
            transform.position = new Vector3(scr_utilities.GetEdge(edgeId.Right, false), transform.position.y);
        if (transform.position.y < scr_utilities.GetEdge(edgeId.Bottom, false))
            transform.position = new Vector3(transform.position.x, scr_utilities.GetEdge(edgeId.Bottom, false));
        if (transform.position.y > scr_utilities.GetEdge(edgeId.Top, false))
            transform.position = new Vector3(transform.position.x, scr_utilities.GetEdge(edgeId.Top, false));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BossCrystal")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other);
        }
        
    }
}