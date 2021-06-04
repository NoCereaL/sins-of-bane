using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int Lives = 3;
    public HealthBarScript healthBar;
    public float RespawnX, RespawnY, RespawnZ;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       
        Death();
        
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
            LoseLife();
        }
        if (Lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
