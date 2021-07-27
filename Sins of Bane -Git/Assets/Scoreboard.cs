using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviourPun
{
    //[SerializeField] Transform playerListContent;
    //[SerializeField] GameObject PlayerListItemPrefab;

    //public static Scoreboard instance;
    //[SerializeField] GameObject sbListingPrefab;
    //[SerializeField] Sprite[] howImages;

    public PlayerInfo player1;

    [SerializeField] Text player1Name;
    [SerializeField] Text player1Deaths;
    [SerializeField] Text player2Name;
    [SerializeField] Text player3Name;
    [SerializeField] Text player4Name;

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }*/
        SetDeaths();
        photonView.RPC("SetNames", RpcTarget.AllBuffered);
        SetNames();
    }

    [PunRPC]
    public void SetNames()
    {
        player1Name.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        player2Name.text = PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
        player3Name.text = PhotonNetwork.CurrentRoom.GetPlayer(3).NickName;
        player4Name.text = PhotonNetwork.CurrentRoom.GetPlayer(4).NickName;
    }

    [PunRPC]
    public void SetDeaths()
    {
        player1Deaths.text = player1.DeathCount +"";
    }
}
