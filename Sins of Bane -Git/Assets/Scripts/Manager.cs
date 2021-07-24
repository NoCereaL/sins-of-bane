using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviourPun
{
    public Scores scores;

    public int teamOneScore;
    public int teamTwoScore;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        teamOneScore = scores.GetComponent<Scores>().TeamOneScore;
        teamTwoScore = scores.GetComponent<Scores>().TeamTwoScore;

        GetWinner();
    }

    public void GetWinner()
    {
        if(teamOneScore == 25 && teamOneScore > teamTwoScore)
        {
            Debug.Log("The Astrolites have Won!!!");
        }
        else if(teamTwoScore == 25 && teamTwoScore > teamOneScore)
        {
            Debug.Log("The Cosniacs have Won!!!");
        }
    }

}
