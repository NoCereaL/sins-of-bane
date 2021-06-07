using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Vector3 mouseCoords;

    public float MouseSensitivity = 1f;

    void Start()
    {
        Cursor.visible = false;    
    }

    // Update is called once per frame
    void Update()
    {
        GameObject crosshair = GameObject.Find("crosshair");
        mouseCoords = Input.mousePosition;
        mouseCoords = Camera.main.ScreenToWorldPoint(mouseCoords);
        crosshair.transform.position = Vector2.Lerp(transform.position, mouseCoords, MouseSensitivity);
        print(mouseCoords);
    }
}
