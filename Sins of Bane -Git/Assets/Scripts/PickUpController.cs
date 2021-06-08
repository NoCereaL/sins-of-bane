using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public PistolWeapons weapon;
    public ARWeapons aRWeapons;
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public Transform player, gunContainer, cam;
    public GunMovement gunPosition;

    public float pickUpRange;
    public float dropForwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
        JoinGameController();
        if (!equipped)
        {
            weapon.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;

            aRWeapons.enabled = false;
        }

        if (equipped)
        {
            weapon.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;

            aRWeapons.enabled = true;
        }
    }

    private void Update()
    {
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
        //Check if player in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();            
        }

        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make Weapon/Gun a child of Weapon and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.2f,0.2f,0f);

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable Sctipt
        weapon.enabled = true;

        aRWeapons.enabled = true;
    }

    void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Set FirePoint to null
        gunPosition.firePoint = null;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        //AddForce
        //rb.AddForce(cam.forward * dropForwardForce, ForceMode2D.Impulse);
        //float random = Random.Range(-1f, 1f);
        //rb.AddTorque(random);

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Disable Sctipt
        weapon.enabled = false;

        aRWeapons.enabled = false;
    }


    //Sets weapon and game parameters for current player
    public void JoinGameController()
    {
        player = GameObject.Find("player1").GetComponent<Transform>();
        gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
