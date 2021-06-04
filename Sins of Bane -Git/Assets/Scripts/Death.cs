using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private GameObject Player;
    public Player player;

    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "spike")
        {
            Player.GetComponent<Respawn>().isDead = true;
            player.currentHealth = player.maxHealth;
            player.healthBar.SetHealth(player.currentHealth);
            player.LoseLife();
            audioData.Play();
        }
        if (collision.collider.tag == "water")
        {
            Player.GetComponent<Respawn>().isDead = true;
            player.currentHealth = player.maxHealth;
            player.healthBar.SetHealth(player.currentHealth);
            player.LoseLife();
            audioData.Play();
        }
        if (collision.collider.tag == "enemy")
        {
            audioData.Play();
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "spike")
        {
            Player.GetComponent<Respawn>().isDead = false;
        }
        if (collision.collider.tag == "water")
        {
            Player.GetComponent<Respawn>().isDead = false;
        }
    }
}
