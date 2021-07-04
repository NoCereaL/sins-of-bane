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
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            SpawnPlayer();
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SpawnPlayer2();
        }
        //SpawnGuns();
        //PhotonNetwork.LocalPlayer.NickName = "player1";
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        //base.OnJoinedRoom();
    }

    public static GameObject myPlayer;
    public static GameObject myPlayer2;
    public void SpawnPlayer()
    {
        myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        myPlayer.GetComponent<AstroMovement>().enabled = true;
        myPlayer.transform.Find("Camera").gameObject.SetActive(true);
        myPlayer.GetComponentInChildren<AstroArmMove>().enabled = true;
        //myPlayer.name = "player1";
    }

    public void SpawnPlayer2()
    {
        myPlayer2 = (GameObject)PhotonNetwork.Instantiate("Player2", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        myPlayer2.GetComponent<AstroMovement>().enabled = true;
        myPlayer2.transform.Find("Camera").gameObject.SetActive(true);
        myPlayer2.GetComponent<AstroArmMove>().enabled = true;
        //myPlayer2.name = "player2";
    }

    public void SpawnPlayer3()
    {
        myPlayer2 = (GameObject)PhotonNetwork.Instantiate("Player3", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        myPlayer2.GetComponent<Movement>().enabled = true;
        myPlayer2.transform.Find("Camera").gameObject.SetActive(true);
        //myPlayer2.name = "player2";
    }

    public GameObject M4;
    public GameObject AR15;
    public GameObject Glock;
    public void SpawnGuns()
    {
        M4 = (GameObject)PhotonNetwork.Instantiate("M4", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        AR15 = (GameObject)PhotonNetwork.Instantiate("AR-15", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        Glock = (GameObject)PhotonNetwork.Instantiate("Glock", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
    }

    public override void OnCreatedRoom()
    {
        //myPlayer.name = "player1";
        //myPlayer2.name = "player2";
        //SpawnGuns();
        //base.OnCreatedRoom();
    }

}
