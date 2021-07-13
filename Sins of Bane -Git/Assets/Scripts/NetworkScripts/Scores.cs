using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Scores : MonoBehaviourPun
{
    public int TeamOneScore;
    public int TeamTwoScore;
    public int PlayerDeath;

    public Text TeamOneText;
    public Text TeamTwoText;

    public TeamBarScript TeamOne;
    public TeamBarScript TeamTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TeamOneText.text = TeamOneScore + "";
        TeamTwoText.text = TeamTwoScore + "";

        TeamOne.SetScore(TeamOneScore);
        TeamTwo.SetScore(TeamTwoScore);

        if (TeamOneScore > TeamTwoScore) {
            TeamOneText.fontSize = 20;
            TeamTwoText.fontSize = 15;
        }
        if(TeamOneScore < TeamTwoScore)
        {
            TeamTwoText.fontSize = 20;
            TeamOneText.fontSize = 15;
        }
        
        photonView.RPC("GetDeaths", RpcTarget.AllBuffered);
        GetDeaths();
    }

    [PunRPC]
    void GetDeaths()
    {
        TeamOneScore = GameObject.Find("player2(Clone)").GetComponent<PlayerInfo>().DeathCount;
        TeamTwoScore = GameObject.Find("player(Clone)").GetComponent<PlayerInfo>().DeathCount;
        /*TeamOneScore = GameObject.Find("player(Clone)").GetComponent<PlayerInfo>().TeamOneScore
            + GameObject.Find("player2(Clone)").GetComponent<PlayerInfo>().TeamOneScore
            + GameObject.Find("player3(Clone)").GetComponent<PlayerInfo>().TeamOneScore
        ;
        /*
        TeamTwoScore = GameObject.Find("player(Clone)").GetComponent<PlayerInfo>().TeamTwoScore
            + GameObject.Find("player2(Clone)").GetComponent<PlayerInfo>().TeamTwoScore
            + GameObject.Find("player3(Clone)").GetComponent<PlayerInfo>().TeamTwoScore
        ;*/
    }

}
