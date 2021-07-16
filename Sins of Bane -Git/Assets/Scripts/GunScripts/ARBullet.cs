using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ARBullet : MonoBehaviourPun
{
    public float speed = 20f;

    public int damage = 5;
    public Rigidbody2D rb;

    public GameObject bullet;

    public int BulletLife = 2;

    public GameObject impactEffect;

    public PlayerInfo player;

    public string Owner;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        if(photonView.IsMine)
        Owner = PhotonNetwork.LocalPlayer.NickName;
    }

    void Update()
    {
        photonView.RPC("GetOwner", RpcTarget.AllBuffered, Owner);
        StartCoroutine(DestroyBullet());
    }

    [PunRPC]
    void GetOwner(string owner)
    {
        Owner = owner;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        player = hitInfo.GetComponent<PlayerInfo>();
        if (player != null)
        {
            player.TakeDamage(damage, Owner, player.name);
            if (player.Team == 1)
            {
                GameObject.Find("Killer").GetComponent<Text>().color = Color.red;
                GameObject.Find("Killed").GetComponent<Text>().color = Color.blue;
            }
            if (player.Team == 2)
            {
                GameObject.Find("Killer").GetComponent<Text>().color = Color.blue;
                GameObject.Find("Killed").GetComponent<Text>().color = Color.red;
            }
            Destroy(bullet);
        }
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(bullet);
            //DestroyImmediate(impactEffect, true);
        }
        
        Destroy(bullet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerInfo player = collision.collider.GetComponent<PlayerInfo>();
        if(collision.collider.tag == "Player")
        {
            player.TakeDamage(damage, Owner, player.name);
            Destroy(bullet);
        }
        Destroy(bullet);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(BulletLife);
        Destroy(bullet);
    }
}
