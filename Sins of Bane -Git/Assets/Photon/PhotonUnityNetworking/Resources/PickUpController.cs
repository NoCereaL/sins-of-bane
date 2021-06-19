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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            this.photonView.RPC("Send", RpcTarget.All);
        }

        //JoinController2();
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
            

            //photonView.RPC("JoinGameController", RpcTarget.AllBuffered);

            distanceToPlayer = player.position - transform.position;

            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                photonView.RPC("JoinGameController", RpcTarget.AllBuffered);
                PickUp();
                photonView.RPC("Player1PickedUP", RpcTarget.AllBuffered);
                Debug.Log("All Successfully Executed");
                EnableScripts();
            }

            if (equipped && Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
                photonView.RPC("Player1Dropped", RpcTarget.All);
                DisableScripts();
            }
        }

        
        else if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            
            player = GameObject.Find("player2(Clone)").GetComponent<Transform>();
            gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
            cam = GameObject.Find("Camera").GetComponent<Transform>();
            gunPosition = GameObject.Find("Weapon2").GetComponent<GunMovement>();
            

            //photonView.RPC("JoinGameController2", RpcTarget.AllBuffered);

            distanceToPlayer = player.position - transform.position;

            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                photonView.RPC("JoinGameController2", RpcTarget.AllBuffered);
                PickUp();
                photonView.RPC("Player2PickedUP", RpcTarget.AllBuffered);
                Debug.Log("All Successfully Executed");
                EnableScripts();
            }

            if (equipped && Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
                photonView.RPC("Player2Dropped", RpcTarget.All);
                DisableScripts();
            }
        }

    }

    [PunRPC]
    void Send()
    {
        Debug.Log("Hello Simon, Message Recievied");
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
        //weapon.enabled = true;

        //aRWeapons.enabled = true;
    }


    [PunRPC]
    void Player1PickedUP()
    {
        Debug.Log("Did it Work?");

        //Make Weapon/Gun a child of Weapon and move it to default position
        gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();
        transform.SetParent(GameObject.Find("Weapon").GetComponent<Transform>());
        //PhotonNetwork.Instantiate(gameObject.name, transform.position, Quaternion.Euler(Vector3.zero));
        Debug.Log("Did it Work?");

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
        Debug.Log("Did it Work?");

        //Make Weapon/Gun a child of Weapon and move it to default position
        gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
        transform.SetParent(GameObject.Find("Weapon2").GetComponent<Transform>());
        //PhotonNetwork.Instantiate(gameObject.name, transform.position, Quaternion.Euler(Vector3.zero));
        Debug.Log("Did it Worj?");


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
    }

    [PunRPC]
    void Player1Dropped()
    {
        transform.SetParent(null);

        //Set FirePoint to null
        gunPosition.firePoint = null;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;
    }

    [PunRPC]
    void Player2Dropped()
    {
        transform.SetParent(null);

        //Set FirePoint to null
        gunPosition.firePoint = null;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;
    }

    void EnableScripts()
    {
        weapon.enabled = true;

        aRWeapons.enabled = true;
    }

    void DisableScripts()
    {
        weapon.enabled = false;

        aRWeapons.enabled = false;
    }


    //Sets weapon and game parameters for current player
    [PunRPC]
    public void JoinGameController()
    {
        player = GameObject.Find("player(Clone)").GetComponent<Transform>();
        gunContainer = GameObject.Find("Weapon").GetComponent<Transform>();
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }

    [PunRPC]
    void JoinGameController2()
    {
        player = GameObject.Find("player2(Clone)").GetComponent<Transform>();
        gunContainer = GameObject.Find("Weapon2").GetComponent<Transform>();
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        gunPosition = GameObject.Find("Weapon2").GetComponent<GunMovement>();
    }
}
