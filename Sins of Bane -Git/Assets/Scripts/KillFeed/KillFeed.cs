using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour
{
    public static KillFeed instance;
    [SerializeField] GameObject killListingPrefab;
    [SerializeField] GameObject killListingWithImagePrefab;
    [SerializeField] Sprite[] howImages;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    //KillFeed.instance.AddNewKillListung("KillerName", "KilledName")
    public void AddNewKillListing(string killer, string killed)
    {
        GameObject temp = Instantiate(killListingPrefab, transform);
        temp.transform.SetSiblingIndex(0);
        KillListing tempListing = temp.GetComponent<KillListing>();
        tempListing.SetNames(killer, killed);
    }

    public void AddNewKillListingWithHow(string killer, string killed, string how)
    {
        GameObject temp = Instantiate(killListingPrefab, transform);
        temp.transform.SetSiblingIndex(0);
        KillListing tempListing = temp.GetComponent<KillListing>();
        tempListing.SetNamesAndHow(killer, killed, how);
    }

    public void AddNewKillListingWithHowImage(string killer, string killed, int howIndex)
    {
        GameObject temp = Instantiate(killListingPrefab, transform);
        temp.transform.SetSiblingIndex(0);
        KillListing tempListing = temp.GetComponent<KillListing>();
        tempListing.SetNamesAndHowImage(killer, killed, howImages[howIndex]);
    }
}
