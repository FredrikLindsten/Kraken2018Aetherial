using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D sprite;
    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(sprite, new Vector2(sprite.width / 2 + 1, sprite.height / 2 + 1), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

    }
}