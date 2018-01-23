using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerMovement : MonoBehaviour {
    KeyCode UpKey = KeyCode.W;
    KeyCode DownKey = KeyCode.S;
    KeyCode RightKey = KeyCode.D;
    KeyCode LeftKey = KeyCode.A;
    private Rigidbody2D rb;
    private float shipSpeed = 0.2f;
    private float inertia = 0.1f;
    private float thrustX = 0.0f;
    private float thrustY = 0.0f;
    private float velX = 0.0f;
    private float velY = 0.0f;
    private float maxThrust = 3.0f;
    private float standStill;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(UpKey) && thrustY < maxThrust) {
            thrustY += shipSpeed;
        } else if(!Input.GetKey(UpKey) && thrustY > standStill) {
            thrustY -= inertia;
        }
        if (Input.GetKey(DownKey) && thrustY > -maxThrust)
        {
            thrustY -= shipSpeed;
        }
        else if (!Input.GetKey(DownKey) && thrustY < standStill)
        {
            thrustY += inertia;
        }
        if (Input.GetKey(RightKey) && thrustX < maxThrust)
        {
            thrustX += shipSpeed;
        }
        else if (!Input.GetKey(RightKey) && thrustX > standStill)
        {
            thrustX -= inertia;
        }
        if (Input.GetKey(LeftKey) && thrustX > -maxThrust)
        {
            thrustX -= shipSpeed;
        }
        else if (!Input.GetKey(LeftKey) && thrustX < standStill)
        {
            thrustX += inertia;
        }
        velX = thrustX;
        velY = thrustY;
        rb.velocity = new Vector2 (velX, velY);

        //Inertia

        



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
