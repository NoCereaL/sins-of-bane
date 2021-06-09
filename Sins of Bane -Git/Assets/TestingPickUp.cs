using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestingPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PickItUp();
    }

    public Transform player;

    void PickItUp()
    {
        player = GameObject.Find("player1").GetComponent<Transform>();

        if (Input.GetKeyDown(KeyCode.L))
        {
            transform.SetParent(player);
            transform.localPosition = new Vector2(0, 0);
            //transform.localPosition = new Vector2(5, 5);
            //transform.localScale = new Vector2(1f, 1f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Hey The SerializeView is Working!!!");
    }

}
