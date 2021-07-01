using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviour
{
    public GameObject player;

    public GameObject weaponHolder;

    public MonoBehaviour[] scriptsToIgnore;

    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }
        if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            player.name = "player1(Clone)";
            weaponHolder.name = "Weapon2";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
