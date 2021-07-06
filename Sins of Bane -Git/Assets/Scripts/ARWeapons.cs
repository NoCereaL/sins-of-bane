using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

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
    public int maxAmmo = 10;
    public int amountOfAmmo = 150;
    public int ammo;
    public int bulletsUsed;
    public float reloadTime = 1.5f;
    public bool isReloading = false;

    public AudioSource audioData;
    public AudioSource reloadSound;
    public AudioSource emptyClip;
    public Text ammoText;
    public GameObject reloadText;
    

    // Start is called before the first frame update
    void Start()
    {
        //audioData = GetComponent<AudioSource>();
        ammo = maxAmmo;
        reloadText.active = false;
    }

    // Update is called once per frame
    void Update()
    {

        JoinGameController();

        if(amountOfAmmo <= 0)
        {
            amountOfAmmo = 0;
        }

        if (Input.GetButtonDown("Fire1") && gun.firePoint != null && pickUpController.equipped == true && isReloading == false)
        {
            //Shoot();
            ShootInServer();
        }

        if (Input.GetButtonDown("Fire1") && gun.firePoint != null && pickUpController.equipped == true && isReloading == false)
        {
            isShooting = true;
            //InvokeRepeating("Shoot", 0.1f, 1f/CurrentFireRate);
            InvokeRepeating("ShootInServer", 0.1f, 1f / CurrentFireRate);
        }
        else if (Input.GetButtonUp("Fire1") && gun.firePoint != null && pickUpController.equipped == true)
        {
            isShooting = false;
            //CancelInvoke("Shoot");
            CancelInvoke("ShootInServer");
        }

        if (pickUpController.equipped == false)
        {
            //CancelInvoke("Shoot");
            CancelInvoke("ShootInServer");
        }
        ammoText.text = ammo + "/" + amountOfAmmo;

        if (ammo < maxAmmo)
        {
            StartCoroutine(Reload());
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
        if (ammo > 0)
        {
            PhotonNetwork.Instantiate("AR_Bullet", firePoint.position, transform.rotation);
            TakeAmmo();
            bulletsUsed = bulletsUsed + 1;
            audioData.Play();
        }
        if(ammo <= 0)
        {
            emptyClip.Play();
            reloadText.active = true;
            ammoText.color = Color.red;
        }
    }

    void TakeAmmo()
    {
        if(ammo >= 1)
        {
            ammo = ammo - 1;
        }
    }

    //Sets weapon and game parameters for current player
    public void JoinGameController()
    {
        gun = GameObject.Find("Weapon").GetComponent<GunMovement>();

        if(PhotonNetwork.LocalPlayer.ActorNumber >= 2)
        {
            gun = GameObject.Find("Weapon2").GetComponent<GunMovement>();
        }
    }

    IEnumerator Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && pickUpController.equipped == true && amountOfAmmo != 0)
        {
            isReloading = true;
            reloadText.active = false;
            
            reloadSound.Play();
            
            yield return new WaitForSeconds(reloadTime);

            if (amountOfAmmo < maxAmmo)
            {
                if (ammo > amountOfAmmo)
                {
                    ammo = maxAmmo;
                    amountOfAmmo = amountOfAmmo - bulletsUsed;
                }
                else
                {
                    ammo = maxAmmo;
                    amountOfAmmo = amountOfAmmo - bulletsUsed;
                }
            }
            else
            {
                ammo = maxAmmo;
                amountOfAmmo = amountOfAmmo - bulletsUsed;
            }
            bulletsUsed = 0;
            isReloading = false;
            ammoText.color = Color.white;
        }
    }

}
