using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Mylobby : MonoBehaviourPunCallbacks
{
	public string PlayerName;
	public GameObject roomPanel;
	public InputField ifName;
	public GameObject required;
	public PlayerName[] playersName;
	//public GameObject namecontent;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void PlayGame()
	{
		if (ifName.text.Length > 0)
		{
			PlayerName = ifName.text;
			PhotonNetwork.LocalPlayer.NickName = PlayerName;
			PhotonNetwork.ConnectUsingSettings();
		}
		else
		{
			required.SetActive(true);
		}
	}
	public void JoinRoom()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	public override void OnConnectedToMaster()
	{
		roomPanel.SetActive(true);
    }
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("Room not found, creating one...");
		string roomName = "Sala001";
		RoomOptions rOp = new RoomOptions();
		rOp.MaxPlayers = 20;
		PhotonNetwork.CreateRoom(roomName, rOp);
	}
	public override void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);

		InvokeRepeating("CheckAllReady", 1, 1);
		//ob.transform.parent = namecontent.transform;
		//PhotonNetwork.LoadLevel("Level1");
	}
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Debug.Log("O jogador "+ newPlayer.NickName+" Entrou na sala");
		
		
	}
	void CheckAllReady()
	{
		playersName = FindObjectsOfType<PlayerName>();
		bool allready = false;
		if (playersName.Length > 1)
		{
			allready = playersName.All(x => x.ready);

			if (allready)
			{
				PhotonNetwork.CurrentRoom.IsOpen = false;
				PhotonNetwork.LoadLevel("Level1");
			}
		}

	}
}

