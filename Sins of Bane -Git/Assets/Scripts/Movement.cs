using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float jumpHeight = 5f;
    
    public bool isGrounded = false;

    public float horizontalMove = 0f;

    public float PlayerScale = 1f;

    public float desiredVelocity;

    public Animator animator;

    public GameObject FirePoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        desiredVelocity = Input.GetAxis("Horizontal") * moveSpeed;
        if (desiredVelocity >= 0.01f)
        {
            transform.localScale = new Vector3(PlayerScale, PlayerScale, PlayerScale);
            FirePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        } else if (desiredVelocity <= -0.01f)
        {
            transform.localScale = new Vector3(-PlayerScale, PlayerScale, PlayerScale);
            FirePoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }
    
}