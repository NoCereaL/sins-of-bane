using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public GameObject scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenScoreboard();
    }

    void OpenScoreboard()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            scoreboard.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }
}
