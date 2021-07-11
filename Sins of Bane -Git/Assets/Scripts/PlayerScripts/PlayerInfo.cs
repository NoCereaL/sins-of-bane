using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerInfo : MonoBehaviourPun
{
    public int maxHealth = 100;
    public int currentHealth;
    public int Lives = 3;
    public HealthBarScript healthBar;
    public float RespawnX, RespawnY, RespawnZ;

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
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth +"%";
        if (Input.GetKeyDown(KeyCode.N))
        {
            photonView.RPC("SendMsg", RpcTarget.All);
        }
        ChangeTeamImage();
        
        Death();
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
    void SendMsg()
    {
        print("Initiating");

        Debug.Log("Successfully Recieved");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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
            photonView.RPC("SetScores", RpcTarget.AllBuffered);
            //LoseLife();
        }/*
        if (Lives <= 0)
        {
            SceneManager.LoadScene(0);
        }*/

    }

    [PunRPC]
    void SetScores()
    {
        if (Team == 1)
        {
            Scores.TeamTwoScore++;
            //cosniacsScore++;
            cosniacsScoreText.text = Scores.TeamTwoScore + "";
            Debug.Log(Scores.TeamTwoScore);
            Debug.Log(Scores.TeamOneScore);
        }
        else if (Team == 2)
        {
            Scores.TeamOneScore++;
            //astrolitionScore++;
            astrolitionScoreText.text = Scores.TeamOneScore + "";
            Debug.Log(Scores.TeamTwoScore);
            Debug.Log(Scores.TeamOneScore);
        }
    }

}
