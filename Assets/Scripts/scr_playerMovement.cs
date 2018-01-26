using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerMovement : MonoBehaviour {
    KeyCode UpKey = KeyCode.W;
    KeyCode DownKey = KeyCode.S;
    KeyCode RightKey = KeyCode.D;
    KeyCode LeftKey = KeyCode.A;
    private Rigidbody2D rb;
    public float shipSpeed;
    public float inertia;
    private float thrustX;
    private float thrustY;
    private float velX;
    private float velY;
    public float maxThrust;
    private const float standStill = 0;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
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




    }
}





/*if (Input.anyKey)
        {

            if (Input.GetKey(UpKey) && rb.velocity.x < 1)
            {
                rb.velocity += new Vector2(0, 0.25f);
            }
            if (Input.GetKey(DownKey) && rb.velocity.x > -1)
            {
                rb.velocity += new Vector2(0, -0.25f);
            }
            if (Input.GetKey(RightKey) && rb.velocity.y < 1)
            {
                rb.velocity += new Vector2(0.25f, 0);
            }
            if (Input.GetKey(LeftKey) && rb.velocity.y > -1)
            {
                rb.velocity += new Vector2(-0.25f, 0);
            }
        } 
*/
