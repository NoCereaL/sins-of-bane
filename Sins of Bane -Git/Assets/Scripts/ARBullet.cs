using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARBullet : MonoBehaviour
{
    public float speed = 20f;

    public int damage = 5;
    public Rigidbody2D rb;

    public GameObject bullet;

    public int BulletLife = 2;

    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        StartCoroutine(DestroyBullet());
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(bullet);
            //DestroyImmediate(impactEffect, true);
        }
        Destroy(bullet);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(BulletLife);
        Destroy(bullet);
    }
}
