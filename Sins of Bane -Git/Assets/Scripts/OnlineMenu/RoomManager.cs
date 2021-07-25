using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
	public static RoomManager Instance;

	public static GameObject myPlayer;
	public static GameObject myPlayer2;
	public static GameObject myPlayer3;

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

		if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
		{
			SpawnPlayer();
			myPlayer.transform.Find("crosshair").gameObject.SetActive(true);
			Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
		}
		if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
		{
			SpawnPlayer2();
			myPlayer2.transform.Find("crosshair").gameObject.SetActive(true);
			Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
		}
		if (PlayerInfo.playerID == 3)
		{
			SpawnPlayer3();
			myPlayer3.transform.Find("crosshair").gameObject.SetActive(true);
			Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
		}
		if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
		{
			//SpawnPlayer2();
			//myPlayer2.transform.Find("crosshair").gameObject.SetActive(true);
		}

	}

	
	public void SpawnPlayer()
	{
		myPlayer = (GameObject)PhotonNetwork.Instantiate("Player", new Vector2(Random.Range(-35f, -10f), transform.position.y), Quaternion.identity);
		myPlayer.GetComponent<AstroMovement>().enabled = true;
		myPlayer.transform.Find("Camera").gameObject.SetActive(true);
		myPlayer.GetComponentInChildren<AstroArmMove>().enabled = true;
		myPlayer.transform.Find("HUD").gameObject.SetActive(true);
		myPlayer.transform.Find("crosshair").gameObject.SetActive(true);
		myPlayer.transform.Find("MiniMapCam").gameObject.SetActive(true);
		myPlayer.GetComponent<PlayerInfo>().Team = 1;
		myPlayer.GetComponent<PlayerInfo>().name = PhotonNetwork.LocalPlayer.NickName;
	}

	public void SpawnPlayer2()
	{
		myPlayer2 = (GameObject)PhotonNetwork.Instantiate("Player2", new Vector2(Random.Range(10f, 35f), transform.position.y), Quaternion.identity);
		myPlayer2.GetComponent<AstroMovement>().enabled = true;
		myPlayer2.transform.Find("Camera").gameObject.SetActive(true);
		myPlayer2.GetComponentInChildren<AstroArmMove>().enabled = true;
		myPlayer2.transform.Find("HUD").gameObject.SetActive(true);
		myPlayer2.transform.Find("crosshair").gameObject.SetActive(true);
		myPlayer2.transform.Find("MiniMapCam").gameObject.SetActive(true);
		myPlayer2.GetComponent<PlayerInfo>().Team = 2;
		myPlayer2.GetComponent<PlayerInfo>().name = PhotonNetwork.LocalPlayer.NickName;
	}

	public void SpawnPlayer3()
	{
		myPlayer3 = (GameObject)PhotonNetwork.Instantiate("Player3", new Vector2(Random.Range(10f, 35f), transform.position.y), Quaternion.identity);
		myPlayer3.GetComponent<AstroMovement>().enabled = true;
		myPlayer3.transform.Find("Camera").gameObject.SetActive(true);
		myPlayer3.GetComponentInChildren<AstroArmMove>().enabled = true;
		myPlayer3.transform.Find("HUD").gameObject.SetActive(true);
		myPlayer3.transform.Find("crosshair").gameObject.SetActive(true);
		myPlayer3.transform.Find("MiniMapCam").gameObject.SetActive(true);
		myPlayer3.GetComponent<PlayerInfo>().Team = 2;
		myPlayer3.GetComponent<PlayerInfo>().name = PhotonNetwork.LocalPlayer.NickName;
		//myPlayer2.name = "player2";
	}
}