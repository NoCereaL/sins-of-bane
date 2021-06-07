using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{


    float angle;

    private Camera camera;

    public GameObject firePoint;

    public Movement movement;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        angle = AngleBetweenTwoPoints(firePoint.transform.position, mouseWorldPosition);

        transform.rotation = Quaternion.Euler(new Vector3(180f, 180f, angle));

        WeaponTransfer();
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void WeaponTransfer()
    {
        if(movement.hasTurned == true)
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }
        if (movement.hasTurned == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
