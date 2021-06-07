using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //transform.Rotate(0, 180, 0);
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}
