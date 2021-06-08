using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        //base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
        //base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-8f,11f), transform.position.y), Quaternion.identity);
        SpawnGuns();
        PhotonNetwork.LocalPlayer.NickName = "player1";
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        //base.OnJoinedRoom();
    }

    public void SpawnGuns()
    {
        PhotonNetwork.Instantiate("M4", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        PhotonNetwork.Instantiate("AR-15", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
    }
}
