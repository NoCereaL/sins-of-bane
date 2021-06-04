using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 100;

    public GameObject deathEffect;

    public int Damage = 20;

    public Player player;

    AudioSource audioData;
    AudioSource audi;
    public GameObject enemies;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audi = enemies.GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioData.Play();
        if (health <= 0)
        {
            Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            HitPlayer();
        }
    }

    public void HitPlayer()
    {
        if (player != null)
        {
            player.TakeDamage(Damage);
        }
    }
    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffect);
        audi.Play();
    }
    
}
