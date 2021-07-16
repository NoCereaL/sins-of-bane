using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillListing : MonoBehaviour
{
    [SerializeField] Text killerDisplay;
    [SerializeField] Text killedDisplay;
    [SerializeField] Text howDisplay;
    [SerializeField] Image howImageDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void SetNames(string killername, string killedName)
    {
        killerDisplay.text = killername;
        killedDisplay.text = killedName;
    }

    public void SetNamesAndHow(string killerName, string killedName, string how)
    {
        killerDisplay.text = killerName;
        killedDisplay.text = killedName;

        howDisplay.text = how;
    }

    public void SetNamesAndHowImage(string killerName, string killedName, Sprite howImage)
    {
        killerDisplay.text = killerName;
        killedDisplay.text = killedName;
        howImageDisplay.sprite = howImage;
    }
}
