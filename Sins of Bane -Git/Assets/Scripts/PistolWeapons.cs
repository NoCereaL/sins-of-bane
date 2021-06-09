using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PistolWeapons : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public GunMovement gun;

    public PickUpController pickUpController;


    public AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        JoinGameController();
        if (Input.GetButtonDown("Fire1") && gun.firePoint != null && pickUpController.equipped == true)
        {
            //Shoot();
            ShootInServer();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        audioData.Play();
    }

    void ShootInServer()
    {
        PhotonNetwork.Instantiate("Bullet", firePoint.position, transform.rotation);
        audioData.Play();
    }

    public void JoinGameController()
    {
        gun = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
