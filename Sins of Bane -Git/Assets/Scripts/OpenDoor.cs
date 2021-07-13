using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float stopDoorTime = 2;
    public float timeMoving;
    public float finalTime;
    public Vector2 currentPosition, openPosition, closePosition;
    Collider2D door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        door = gameObject.GetComponent<Collider2D>();
        if (Input.GetKeyDown(KeyCode.F))
        {
            InvokeRepeating("MoveDoor",0f,0.01f);
            StartCoroutine(StopDoor());
            StartCoroutine(WaitAndClose());
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            InvokeRepeating("CloseDoor", 0f, 0.01f);
            StartCoroutine(StopDoor());
        }

    }

    void MoveDoor()
    {
        timeMoving += Time.timeScale/100;

        currentPosition = new Vector2(transform.position.x, transform.position.y);
        openPosition = new Vector2(transform.position.x, transform.position.y + 5f);
        
        transform.position = Vector2.Lerp(currentPosition, openPosition, 0.01f);
    }
    void CloseDoor()
    {
        timeMoving += Time.timeScale / 100;

        currentPosition = new Vector2(transform.position.x, transform.position.y);
        closePosition = new Vector2(transform.position.x, transform.position.y - 5f);

        transform.position = Vector2.Lerp(currentPosition, closePosition, 0.01f);

    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(stopDoorTime);
        finalTime = timeMoving;

        timeMoving = 0f;
        CancelInvoke("MoveDoor");
        CancelInvoke("CloseDoor");
    }

    IEnumerator WaitAndClose()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(StopDoor());
        InvokeRepeating("CloseDoor", 0f, 0.01f);
        yield return new WaitForSeconds(2);
        door.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            door.isTrigger = true;
            InvokeRepeating("MoveDoor", 0f, 0.01f);
            StartCoroutine(StopDoor());
            StartCoroutine(WaitAndClose());
        }
    }
}
