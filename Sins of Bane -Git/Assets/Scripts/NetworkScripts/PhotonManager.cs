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
        SpawnPlayer();
        SpawnGuns();
        //PhotonNetwork.LocalPlayer.NickName = "player1";
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        //base.OnJoinedRoom();
    }

    public void SpawnPlayer()
    {
        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        myPlayer.GetComponent<Movement>().enabled = true;
        myPlayer.transform.FindChild("Camera").gameObject.SetActive(true);
    }

    public GameObject M4;
    public GameObject AR15;
    public GameObject Glock;
    public void SpawnGuns()
    {
        M4 = (GameObject)PhotonNetwork.Instantiate("M4", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        M4.GetComponent<PickUpController>().enabled = true;
        AR15 = (GameObject)PhotonNetwork.Instantiate("AR-15", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
        Glock = (GameObject)PhotonNetwork.Instantiate("Glock", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
    }
}
