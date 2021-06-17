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
        JoinController2();
        ExecuteTheUpdate();

        /*
        JoinGameController();

        //Check if player in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.UserId);
            PickUp();
            //newPickUp();
            photonView.RPC("newPickUp", RpcTarget.OthersBuffered);
        }

        if(equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
        */
    }

    void ExecuteTheUpdate()
    {
        Vector3 distanceToPlayer;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {

            player = GameObject.Find("player(Clone)").GetComponent<Transform>();
            gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();
            cam = GameObject.Find("Camera").GetComponent<Transform>();
            gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();

            distanceToPlayer = player.position - transform.position;

            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                Debug.Log(PhotonNetwork.LocalPlayer.UserId);
                PickUp();
                //newPickUp();
                photonView.RPC("Player1PickedUp", RpcTarget.AllBuffered);
                Player1PickedUP();
            }

            if (equipped && Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
        }


        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {

            player = GameObject.Find("player2(Clone)").GetComponent<Transform>();
            gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
            cam = GameObject.Find("Camera").GetComponent<Transform>();
            gunPosition = GameObject.Find("Weapon2").GetComponent<GunMovement>();

            distanceToPlayer = player.position - transform.position;

            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.R) && !slotFull && PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                Debug.Log(PhotonNetwork.LocalPlayer.UserId);
                PickUp();
                //newPickUp();
                photonView.RPC("Player2PickedUp", RpcTarget.AllBuffered);
                Player2PickedUP();
            }

            if (equipped && Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
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

    [PunRPC]
    void newPickUp()
    {
        equipped = true;
        slotFull = true;

        //Make Weapon/Gun a child of Weapon and move it to default position
        if (Input.GetKeyDown(KeyCode.E))
        {
            gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();
            transform.SetParent(gunContainer);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
            transform.SetParent(gunContainer);
        }
        //transform.SetParent(gunContainer);
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

    [PunRPC]
    void Player1PickedUP()
    {
        //Make Weapon/Gun a child of Weapon and move it to default position
        gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();
        transform.SetParent(GameObject.Find("Weapon").GetComponent<Transform>());
        
        //transform.SetParent(gunContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.2f, 0.2f, 0f);

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    [PunRPC]
    void Player2PickedUP()
    {
        //Make Weapon/Gun a child of Weapon and move it to default position
        gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
        transform.SetParent(GameObject.Find("Weapon2").GetComponent<Transform>());

        //transform.SetParent(gunContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.2f, 0.2f, 0f);

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
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
        player = GameObject.Find("player(Clone)").GetComponent<Transform>();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            player = GameObject.Find("player2(Clone)").GetComponent<Transform>();
        }

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        //Gun Containers
        gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
        }


        //Camera
        cam = GameObject.Find("Camera").GetComponent<Transform>();


        //Gun Positions
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            gunPosition = GameObject.Find("Weapon2").GetComponent<GunMovement>();
        }
    }

    [PunRPC]
    void JoinController2()
    {
        player = GameObject.Find("player(Clone)").GetComponent<Transform>();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            player = GameObject.Find("player2(Clone)").GetComponent<Transform>();
        }

        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        //Gun Containers
        gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
        }


        //Camera
        cam = GameObject.Find("Camera").GetComponent<Transform>();


        //Gun Positions
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();

        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            gunPosition = GameObject.Find("Weapon2").GetComponent<GunMovement>();
        }
    }
}
