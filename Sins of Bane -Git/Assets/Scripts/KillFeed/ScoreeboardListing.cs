using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class ScoreeboardListing : MonoBehaviour
{
    /*
    [SerializeField] Text playerDisplay;
    [SerializeField] Text killsDisplay;
    [SerializeField] Text deathsDisplay;
    [SerializeField] Image howImageDisplay;
    */

    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject PlayerListItemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    /*
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
    */
}
