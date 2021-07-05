using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
	public static RoomManager Instance;

	void Awake()
	{
		if(Instance)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if(scene.buildIndex == 1) // We're in the game scene
		{
			//PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
		}
		if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
			//GameObject ball = (GameObject)PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ball"), Vector3.zero, Quaternion.identity);
			//ball.GetComponent<BallMovement>().enabled = true;
		}

		if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
		{
			SpawnPlayer();
		}
		if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
		{
			SpawnPlayer2();
		}

	}

	public void SpawnPlayer()
	{
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("Player1", new Vector3(8, 0, -1), Quaternion.identity);
		myPlayer.GetComponent<Movement>().enabled = true;
	}

	public void SpawnPlayer2()
	{
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate("Player2", new Vector3(-8, 0, -1), Quaternion.identity);
		myPlayer.GetComponent<Movement>().enabled = true;
	}
}