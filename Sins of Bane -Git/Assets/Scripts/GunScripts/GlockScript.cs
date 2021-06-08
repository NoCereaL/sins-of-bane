using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockScript : MonoBehaviour
{

    public PickUpController pickUpController;
    public GameObject Glock;
    public GunMovement gunPosition;

    public Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;

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
            gunPosition.firePoint = Glock;
            boxCollider.enabled = false;
            rigidbody.simulated = false;
        }
        else if(pickUpController.equipped == false)
        {
            boxCollider.enabled = true;
            rigidbody.simulated = true;
        }
    }

    public void JoinGameController()
    {
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
