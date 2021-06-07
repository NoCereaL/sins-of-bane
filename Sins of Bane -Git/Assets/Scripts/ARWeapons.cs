using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWeapons : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public GunMovement gun;

    public PickUpController pickUpController;


    public AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        //audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && gun.firePoint != null && pickUpController.equipped == true)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        audioData.Play();
    }
}
