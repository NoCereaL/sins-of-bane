using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AstroArmMove : MonoBehaviour
{
    public static float angle;

    public GameObject firePoint;

    public MonoBehaviour[] scriptsToIgnore;

    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);
        try
        {
            angle = AngleBetweenTwoPoints(firePoint.transform.position, mouseWorldPosition);
        }
        catch { }
        transform.rotation = Quaternion.Euler(new Vector3(180f, 180f, angle));
        FlipHand();
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void FlipHand()
    {
        float center = Screen.width - (Screen.width / 2);

        if (Input.mousePosition.x >= center)
        {
            transform.localScale = new Vector3(AstroMovement.PlayerScale, AstroMovement.PlayerScale, AstroMovement.PlayerScale);            
        }
        else if (Input.mousePosition.x <= center)
        {
            transform.localScale = new Vector3(-AstroMovement.PlayerScale, -AstroMovement.PlayerScale, AstroMovement.PlayerScale);
        }
    }
    
}
