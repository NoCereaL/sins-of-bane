using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;

	GameObject controller;

	GameObject myPlayer;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if(PV.IsMine)
		{
			//CreateController();
		}

		if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
			CreateController();
        }
		if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
		{
			CreateController2();
		}
	}

	void CreateController()
	{
		Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
		//controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(8,0,-1), spawnpoint.rotation, 0, new object[] { PV.ViewID });

		myPlayer = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(8, 0, -1), Quaternion.identity);
		myPlayer.GetComponent<Movement>().enabled = true;
		//controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(-8,0,-1), spawnpoint.rotation, 0, new object[] { PV.ViewID });

	}

	void CreateController2()
	{
		myPlayer = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(-8, 0, -1), Quaternion.identity);
		myPlayer.GetComponent<Movement>().enabled = true;
		//controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(-8,0,-1), spawnpoint.rotation, 0, new object[] { PV.ViewID });
	}

	public void Die()
	{
		PhotonNetwork.Destroy(controller);
		CreateController();
	}
}