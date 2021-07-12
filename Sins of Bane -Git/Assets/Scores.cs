using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Scores : MonoBehaviourPun
{
    public int TeamOneScore;
    public int TeamTwoScore;

    public Text TeamOneText;
    public Text TeamTwoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TeamOneText.text = TeamOneScore + "";
        TeamTwoText.text = TeamTwoScore + "";
        
        photonView.RPC("GetDeaths", RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    void GetDeaths()
    {
        TeamOneScore = TeamOneScore + GameObject.Find("player(Clone)").GetComponent<PlayerInfo>().DeathCount;
        TeamTwoScore = TeamTwoScore + GameObject.Find("player2(Clone)").GetComponent<PlayerInfo>().DeathCount;

    }

}
