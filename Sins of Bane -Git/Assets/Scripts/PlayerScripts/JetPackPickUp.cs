using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JetPackPickUp : MonoBehaviourPun
{
    public Transform player, jetPackContainer, cam;
    public Rigidbody2D rb;
    public BoxCollider2D coll;

    public float pickUpRange;
    public static bool equipped;
    public static bool slotFull;
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        JoinGameController();
        HUD.active = false;
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;

        }

        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteTheUpdate();
    }


    void ExecuteTheUpdate()
    {
        Vector2 distanceToPlayer;

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {

            player = GameObject.Find("player(Clone)").GetComponent<Transform>();
            jetPackContainer = GameObject.Find("JetPackHolder").GetComponent<Transform>();
            cam = GameObject.Find("Camera").GetComponent<Transform>();


            distanceToPlayer = player.position - transform.position;

            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
            {
                photonView.RPC("JoinGameController", RpcTarget.AllBuffered);
                PickUp();
                photonView.RPC("Player1PickedUP", RpcTarget.AllBuffered);
                photonView.RPC("TransformViewOff", RpcTarget.AllBuffered);
            }

            if (equipped && Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
                photonView.RPC("Player1Dropped", RpcTarget.All);
                photonView.RPC("TransformViewOn", RpcTarget.AllBuffered);
            }
        }


        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {

            player = GameObject.Find("player2(Clone)").GetComponent<Transform>();
            jetPackContainer = GameObject.Find("JetPackHolder2").GetComponent<Transform>();
            cam = GameObject.Find("Camera").GetComponent<Transform>();


            //photonView.RPC("JoinGameController2", RpcTarget.AllBuffered);

            distanceToPlayer = player.position - transform.position;

            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                photonView.RPC("JoinGameController2", RpcTarget.AllBuffered);
                PickUp();
                photonView.RPC("Player2PickedUP", RpcTarget.AllBuffered);
                photonView.RPC("TransformViewOff", RpcTarget.AllBuffered);
            }

            if (equipped && Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
                photonView.RPC("Player2Dropped", RpcTarget.All);
                photonView.RPC("TransformViewOn", RpcTarget.AllBuffered);
            }
        }

    }


    void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(jetPackContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(1f, 1f, transform.localScale.z);

        rb.isKinematic = true;
        coll.isTrigger = true;

        HUD.active = true;
    }

    [PunRPC]
    void Player1PickedUP()
    {
        //Make Weapon/Gun a child of Weapon and move it to default position
        jetPackContainer = GameObject.Find("JetPackHolder").GetComponent<Transform>();
        transform.SetParent(GameObject.Find("JetPackHolder").GetComponent<Transform>());
        //PhotonNetwork.Instantiate(gameObject.name, transform.position, Quaternion.Euler(Vector3.zero));

        //transform.SetParent(gunContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(1f, 1f, transform.localScale.z);

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
        photonView.TransferOwnership(1);
    }

    [PunRPC]
    void Player2PickedUP()
    {

        //Make Weapon/Gun a child of Weapon and move it to default position
        jetPackContainer = GameObject.Find("JetPackHolder2").GetComponent<Transform>();
        transform.SetParent(GameObject.Find("JetPackHolder2").GetComponent<Transform>());
        //PhotonNetwork.Instantiate(gameObject.name, transform.position, Quaternion.Euler(Vector3.zero));


        //transform.SetParent(gunContainer);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(1f, 1f, transform.localScale.z);

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
        photonView.TransferOwnership(2);
    }

    [PunRPC]
    void TransformViewOff()
    {
        gameObject.GetComponent<PhotonTransformView>().enabled = false;
    }

    [PunRPC]
    void TransformViewOn()
    {
        gameObject.GetComponent<PhotonTransformView>().enabled = true;
    }

    void Drop() {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;

        HUD.active = false;
    }

    [PunRPC]
    void Player1Dropped()
    {
        transform.SetParent(null);

        //Set FirePoint to null

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

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        //Make Rigidbody2D kinematic and BoxCollider2D a trigger
        rb.isKinematic = false;
        coll.isTrigger = false;
    }


    [PunRPC]
    public void JoinGameController()
    {
        player = GameObject.Find("player(Clone)").GetComponent<Transform>();
        jetPackContainer = GameObject.Find("JetPackHolder").GetComponent<Transform>();
        cam = GameObject.Find("Camera").GetComponent<Transform>();
        //gunPosition = GameObject.Find("Weapon").GetComponent<GunMovement>();
    }
}
