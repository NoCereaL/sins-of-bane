using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerInfo : MonoBehaviourPun
{
    public string name;
    public static int playerID;
    public int maxHealth = 100;
    public int currentHealth;
    public int Lives = 3;
    public HealthBarScript healthBar;
    public float RespawnX, RespawnY, RespawnZ;
    public int DeathCount;
    public int TeamOneScore;
    public int TeamTwoScore;

    public GameObject player;
    public Text healthText;

    public int Team;
    public Text teamName;
    public Image TeamLogo;
    public Sprite Cosniacs;
    public Sprite Astrolition;
    public int cosniacsScore;
    public int astrolitionScore;
    public Text cosniacsScoreText;
    public Text astrolitionScoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (photonView.IsMine)
        {
            playerID = PhotonNetwork.CurrentRoom.PlayerCount;
        }
        Debug.Log("Player ID: " +playerID);
    }

    // Update is called once per frame
    void Update()
    {
        //photonView.RPC("GetName", RpcTarget.OthersBuffered, name);

        //photonView.RPC("DeathCounter", RpcTarget.AllBuffered, DeathCount);
        //photonView.RPC("TeamOneScores", RpcTarget.AllBuffered, TeamOneScore);
        //photonView.RPC("TeamTwoScores", RpcTarget.AllBuffered, TeamTwoScore);
        //photonView.RPC("UpdateHealth", RpcTarget.OthersBuffered, currentHealth);
        healthText.text = currentHealth +"%";
        
        ChangeTeamImage();
        Death();
    }

    [PunRPC]
    void GetName(string n)
    {
        name = n;
    }

    [PunRPC]
    void UpdateHealth(int health)
    {
        currentHealth = health;
    }

    [PunRPC]
    void DeathCounter(int deaths)
    {
        DeathCount = deaths;
    }

    [PunRPC]
    void TeamOneScores(int score)
    {
        TeamOneScore = score;
    }
    [PunRPC]
    void TeamTwoScores(int score)
    {
        TeamTwoScore = score;
    }

    void ChangeTeamImage()
    {
        if (Team == 1)
        {
            TeamLogo.sprite = Astrolition;
            teamName.text = "Astrolition";
        }
        else if(Team == 2)
        {
            TeamLogo.sprite = Cosniacs;
            teamName.text = "Cosniacs";
        }
    }

    [PunRPC]
    void UpdateKillFeed(string killer, string killed)
    {
        KillFeed.instance.AddNewKillListing(killer, killed);
    }

    public void TakeDamage(int damage, string killer, string killed)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth == 0 && photonView.IsMine)
        {
            photonView.RPC("UpdateKillFeed", RpcTarget.AllBuffered, killer, killed);
        }
    }

    public void LoseLife()
    {
        Lives = Lives - 1;
    }

    public void respawn()
    {
        transform.position = new Vector3(RespawnX, RespawnY, RespawnZ);
        transform.rotation = new Quaternion(0, 0, 0, 0);
     }

    public void Death()
    {
        if (currentHealth <= 0)
        {
            respawn();
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            DeathCount++;
            SetDeaths();
            //localDeathCount++;
            //LoseLife();
        }/*
        if (Lives <= 0)
        {
            SceneManager.LoadScene(0);
        }*/
    }


    //Updates Scores to server when the player death is confirmed
    void SetDeaths()
    {

        if(Team == 1)
        {
            TeamTwoScore += 1;
            GameObject.Find("GameManager").GetComponent<Scores>().TeamTwoScore += 1;
        }
        if (Team == 2)
        {
            TeamOneScore += 1;
            GameObject.Find("GameManager").GetComponent<Scores>().TeamOneScore += 1;
        }
        photonView.RPC("GetScoreOne", RpcTarget.AllBuffered, GameObject.Find("GameManager").GetComponent<Scores>().TeamOneScore);
        photonView.RPC("GetScoreTwo", RpcTarget.AllBuffered, GameObject.Find("GameManager").GetComponent<Scores>().TeamTwoScore);
    }

    [PunRPC]
    void GetScoreOne(int score)
    {
        GameObject.Find("GameManager").GetComponent<Scores>().TeamOneScore = score;
    }

    [PunRPC]
    void GetScoreTwo(int score)
    {
        GameObject.Find("GameManager").GetComponent<Scores>().TeamTwoScore = score;
    }

}
