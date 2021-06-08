using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ARWeapons : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public GunMovement gun;

    public PickUpController pickUpController;
    private bool isShooting;
    float nextShot;
    public M4Script m4;
    public AR15Script AR15;
    public float CurrentFireRate;

    public AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        //audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        JoinGameController();
        if (Input.GetButtonDown("Fire1") && gun.firePoint != null && pickUpController.equipped == true)
        {
            Shoot();
            ShootInServer();
        }

        if (Input.GetButtonDown("Fire1") && gun.firePoint != null && pickUpController.equipped == true)
        {
            isShooting = true;
            InvokeRepeating("Shoot", 0.1f, 1f/CurrentFireRate);
            //InvokeRepeating("ShootInServer", 0.1f, 1f / CurrentFireRate);
        }
        else if (Input.GetButtonUp("Fire1") && gun.firePoint != null && pickUpController.equipped == true)
        {
            isShooting = false;
            CancelInvoke("Shoot");
            //CancelInvoke("ShootInServer");
        }

        if (pickUpController.equipped == false)
        {
            CancelInvoke("Shoot");
            //CancelInvoke("ShootInServer");
        }

    }
    
    void Shoot()
    {
        Debug.Log("Shoot");
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        audioData.Play();
    }

    void ShootInServer()
    {
        PhotonNetwork.Instantiate("AR_Bullet", firePoint.position, transform.rotation);
        audioData.Play();
    }

    //Sets weapon and game parameters for current player
    public void JoinGameController()
    {
        gun = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
