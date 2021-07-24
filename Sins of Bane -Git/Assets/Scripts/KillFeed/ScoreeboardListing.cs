using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreeboardListing : MonoBehaviour
{
    [SerializeField] Text playerDisplay;
    [SerializeField] Text killsDisplay;
    [SerializeField] Text deathsDisplay;
    [SerializeField] Image howImageDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void SetName(string killername, string killedName)
    {
        playerDisplay.text = killername;
        killsDisplay.text = killedName;
    }

    public void SetKillsAndDeaths(string killerName, string kills, string deaths)
    {
        playerDisplay.text = killerName;
        killsDisplay.text = kills;

        deathsDisplay.text = deaths;
    }

    public void SetNamesAndHowImage(string killerName, string killedName, Sprite howImage)
    {
        playerDisplay.text = killerName;
        killsDisplay.text = killedName;
        howImageDisplay.sprite = howImage;
    }
}
