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
        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            SpawnPlayer3();
        }
        //SpawnGuns();
        //PhotonNetwork.LocalPlayer.NickName = "player1";
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        //base.OnJoinedRoom();
    }

    public static GameObject myPlayer;
    public static GameObject myPlayer2;
    public static GameObject myPlayer3;

    public void SpawnPlayer()
    {
        myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-35f, -10f), transform.position.y), Quaternion.identity);
        myPlayer.GetComponent<AstroMovement>().enabled = true;
        myPlayer.transform.Find("Camera").gameObject.SetActive(true);
        myPlayer.GetComponentInChildren<AstroArmMove>().enabled = true;
        myPlayer.transform.Find("HUD").gameObject.SetActive(true);
        myPlayer.transform.Find("crosshair").gameObject.SetActive(true);
        myPlayer.transform.Find("MiniMapCam").gameObject.SetActive(true);
        myPlayer.GetComponent<PlayerInfo>().Team = 1;
        //myPlayer.name = "player1";
    }

    public void SpawnPlayer2()
    {
        myPlayer2 = (GameObject)PhotonNetwork.Instantiate("Player2", new Vector2(Random.Range(10f, 35f), transform.position.y), Quaternion.identity);
        myPlayer2.GetComponent<AstroMovement>().enabled = true;
        myPlayer2.transform.Find("Camera").gameObject.SetActive(true);
        myPlayer2.GetComponentInChildren<AstroArmMove>().enabled = true;
        myPlayer2.transform.Find("HUD").gameObject.SetActive(true);
        myPlayer2.transform.Find("crosshair").gameObject.SetActive(true);
        myPlayer2.transform.Find("MiniMapCam").gameObject.SetActive(true);
        myPlayer2.GetComponent<PlayerInfo>().Team = 2;
        //myPlayer2.name = "player2";
    }

    public void SpawnPlayer3()
    {
        myPlayer3 = (GameObject)PhotonNetwork.Instantiate("Player3", new Vector2(Random.Range(10f, 35f), transform.position.y), Quaternion.identity);
        myPlayer3.GetComponent<AstroMovement>().enabled = true;
        myPlayer3.transform.Find("Camera").gameObject.SetActive(true);
        myPlayer3.GetComponentInChildren<AstroArmMove>().enabled = true;
        myPlayer3.transform.Find("HUD").gameObject.SetActive(true);
        myPlayer3.transform.Find("crosshair").gameObject.SetActive(true);
        myPlayer3.transform.Find("MiniMapCam").gameObject.SetActive(true);
        myPlayer3.GetComponent<PlayerInfo>().Team = 2;
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
