using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class OnlineLauncher : MonoBehaviourPunCallbacks
{
	public static OnlineLauncher Instance;

	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] Text errorText2;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Text roomNameText2;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;
	[SerializeField] Text playerCount;
	[SerializeField] Text globalCount;

	public AudioSource audioClick;
	public AudioSource audioLeave;
	public AudioSource audioStartGame;
	public AudioSource audioPlayerJoined;
	public AudioSource audioPlayerLeft;

	[SerializeField] GameObject LoadingMenu;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
	}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
			RefreshList();
        }
    }

    public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		PhotonNetwork.JoinLobby();
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnJoinedLobby()
	{
		if(LoadingMenu.active == true) 
			MenuManager.Instance.OpenMenu("title");
			Debug.Log("Joined Lobby");
		//PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");	
	}

	public void RefreshList()
	{
		PhotonNetwork.LeaveLobby();
		Debug.Log("Left Lobby");
		PhotonNetwork.JoinLobby();
		Debug.Log("Joined Lobby");
		MenuManager.Instance.OpenMenu("find room");
    }

	[PunRPC]
	public void RefreshListRPC()
	{
		PhotonNetwork.LeaveLobby();
		Debug.Log("Left Lobby");
		PhotonNetwork.JoinLobby();
		Debug.Log("Joined Lobby");
		MenuManager.Instance.OpenMenu("find room");
	}

	public void CreateRoom()
	{
		audioClick.Play();
		if (string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}
		PhotonNetwork.CreateRoom(roomNameInputField.text);
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnJoinedRoom()
	{
		RefreshList();
		MenuManager.Instance.OpenMenu("room");
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;
		roomNameText2.text = PhotonNetwork.CurrentRoom.Name;
		playerCount.text = PhotonNetwork.CurrentRoom.PlayerCount + "/8 - " + "Players"; 

		Player[] players = PhotonNetwork.PlayerList;

		foreach (Transform child in playerListContent)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < players.Count(); i++)
		{
			Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}

		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		errorText2.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		MenuManager.Instance.OpenMenu("error");
	}

	public void StartGame()
	{
		audioStartGame.Play();
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		audioLeave.Play();
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");
	}

	public void JoinRoom(RoomInfo info)
	{
		audioClick.Play();
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}

	public void Cancel()
    {
		RefreshList();
    }

    public override void OnLeftRoom()
	{
		MenuManager.Instance.OpenMenu("title");
	}

	void DestroyListItem()
    {
		foreach (Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}
	}

	
	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		globalCount.text = "Currently Online: " + PhotonNetwork.CountOfPlayers;
		DestroyListItem();

		for (int i = 0; i < roomList.Count; i++)
		{
			if (roomList[i].RemovedFromList)
            {
				RefreshList();
				//continue;
			}            
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}

	public override void OnCreatedRoom()
	{
		RefreshList();
		StartCoroutine(BeginRefresh());
	}

    public override void OnDisconnected(DisconnectCause cause)
    {
		errorText.text = "Disconnected: " + cause;
		errorText2.text = "Disconnected: " + cause;
		Debug.LogError("Disconnected: " + cause);
		MenuManager.Instance.OpenMenu("error");
	}

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
		errorText.text = "Join Room Failed: " + message;
		errorText2.text = "Join Room Failed: " + message;
		Debug.LogError("Join Room Failed: " + message);
		MenuManager.Instance.OpenMenu("error");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		playerCount.text = PhotonNetwork.CurrentRoom.PlayerCount + "/8 - " + "Players";
		audioPlayerJoined.Play();
		Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
		DestroyListItem();
	}

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
		playerCount.text = PhotonNetwork.CurrentRoom.PlayerCount + "/8 - " + "Players";
		audioPlayerLeft.Play();
		DestroyListItem();
    }

	IEnumerator BeginRefresh()
    {
		yield return new WaitForSeconds(2);
		RefreshList();
		Debug.Log("List Refreshed after 2 secs");
    }
}