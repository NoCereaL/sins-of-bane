using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4Script : MonoBehaviour
{

    public PickUpController pickUpController;
    public GameObject AR;
    public GunMovement gunPosition;
    public Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;
    public ARWeapons arWeaponsFireRate;
    public float fireRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        JoinGameController();
        if (pickUpController.equipped == true)
        {
            gunPosition.firePoint = AR;
            boxCollider.enabled = false;
            rigidbody.simulated = false;
            arWeaponsFireRate.CurrentFireRate = fireRate;
        }
        else if (pickUpController.equipped == false)
        {
            boxCollider.enabled = true;
            rigidbody.simulated = true;
        }
    }

    //Sets weapon and game parameters for current player
    public void JoinGameController()
    {
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
