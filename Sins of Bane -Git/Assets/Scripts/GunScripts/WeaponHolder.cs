using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class WeaponHolder : MonoBehaviour, IPunObservable
{
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
        photonView.RPC("Turn", RpcTarget.OthersBuffered);
    }

    [PunRPC]
    void Turn()
    {
        if(Player.player.transform.localScale.x == -1)
        {
            transform.localScale = new Vector3(-1, -1, 1);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
