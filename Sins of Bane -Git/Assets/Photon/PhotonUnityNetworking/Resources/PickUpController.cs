using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PickUpController : MonoBehaviourPun
{
    public PistolWeapons weapon;
    public ARWeapons aRWeapons;
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public Transform player, gunContainer, cam;
    public GunMovement gunPosition;
    public Vector3 MyPosition;

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
        JoinGameController();
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
        //Check if player in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.UserId);
            //PickUp();
            //newPickUp();
            photonView.RPC("newPickUp", RpcTarget.AllBuffered);
        }

        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    [PunRPC]
    void SetParent()
    {
        photonView.transform.SetParent(player);
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        
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

    [PunRPC]
    void newPickUp()
    {
        equipped = true;
        slotFull = true;

        //Make Weapon/Gun a child of Weapon and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.2f, 0.2f, 0f);

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
        //player = GameObject.Find("player1").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();

        gunContainer = player.transform.GetChild(5);


        if (player.name == "player(Clone)")
        {
           // gunContainer.FindChild("player(Clone)").Find("Weapon").GetComponent<Transform>();
        }

        if (player.name == "player2(Clone)")
        {
            gunContainer.FindChild("player2(Clone)").Find("Weapon").GetComponent<Transform>();
        }
        
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
