using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviourPun
{
    public int TeamOneScore;
    public int TeamTwoScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        photonView.RPC("GetScores", RpcTarget.All);
    }

    [PunRPC]
    void GetScores() {
        TeamOneScore = GameObject.Find("player(Clone)").GetComponent<PlayerInfo>().DeathCount;
        TeamTwoScore = GameObject.Find("player2(Clone)").GetComponent<PlayerInfo>().DeathCount;
    }

}
