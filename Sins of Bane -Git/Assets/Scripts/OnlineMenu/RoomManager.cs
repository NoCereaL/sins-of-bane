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

	public static GameObject myPlayer;
	public static GameObject myPlayer2;
	public void SpawnPlayer()
	{
		myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
		myPlayer.GetComponent<AstroMovement>().enabled = true;
		myPlayer.transform.Find("Camera").gameObject.SetActive(true);
		myPlayer.GetComponentInChildren<AstroArmMove>().enabled = true;
		myPlayer.transform.Find("HUD").gameObject.SetActive(true);
	}

	public void SpawnPlayer2()
	{
		myPlayer2 = (GameObject)PhotonNetwork.Instantiate("Player2", new Vector2(Random.Range(-8f, 11f), transform.position.y), Quaternion.identity);
		myPlayer2.GetComponent<AstroMovement>().enabled = true;
		myPlayer2.transform.Find("Camera").gameObject.SetActive(true);
		myPlayer2.GetComponentInChildren<AstroArmMove>().enabled = true;
		myPlayer2.transform.Find("HUD").gameObject.SetActive(true);
	}
}