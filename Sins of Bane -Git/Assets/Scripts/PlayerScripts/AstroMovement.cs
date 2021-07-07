using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AstroMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float jumpHeight = 5f;

    public bool isGrounded = false;

    public bool hasTurned;

    public float horizontalMove = 0f;

    public float heighOfJump;

    public static float PlayerScale = 1f;

    public Animator animator;

    public GameObject FirePoint;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        moveTransform();
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
        if (Input.GetButtonDown("Jump") && (isGrounded == true || rb.velocity.y == 0))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown("w") && (isGrounded == true || rb.velocity.y == 0))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
        heighOfJump = rb.velocity.y;
        
        animator.SetFloat("VerticalVelocity", Mathf.Abs(heighOfJump));

    }

    void moveTransform()
    {
        float center = Screen.width - (Screen.width / 2);

        if (Input.mousePosition.x >= center)
        {
            hasTurned = false;
            transform.localScale = new Vector3(PlayerScale, PlayerScale, PlayerScale);
            FirePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.mousePosition.x <= center)
        {
            hasTurned = true;
            transform.localScale = new Vector3(-PlayerScale, PlayerScale, PlayerScale);
            FirePoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Teleporter" && PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            transform.position = SpawnPoints.spawnPoint[0];
        }
        if (collision.collider.tag == "Teleporter" && PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            transform.position = SpawnPoints.spawnPoint[1];
        }
    }

}


