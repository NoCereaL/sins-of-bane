using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    public GameObject OneLife;
    public GameObject TwoLife;
    public GameObject ThreeLife;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetLive();
    }

    public void SetLive()
    {
        if (player.Lives <= 2)
        {
            Destroy(ThreeLife);
        }
        if (player.Lives <= 1)
        {
            Destroy(TwoLife);
        }
    }
}
