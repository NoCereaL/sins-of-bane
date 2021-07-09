using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    void Fly()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }
}
