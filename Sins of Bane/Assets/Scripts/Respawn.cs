using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        respawn();
    }

    public void respawn()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (transform.position.y < -20 || isDead == true)
        {
            transform.position = new Vector3(-8, -0.1f, -1);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
