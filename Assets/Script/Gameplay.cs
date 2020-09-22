using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gameplay : MonoBehaviour
{
	public GameObject[] respawns;
	TankId[] tanks;
	public PhotonView pview;
	public GameObject winner;
	public GameRoom gameRoom;
	public string playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
		Invoke("StartGame", 3);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	void StartGame()
	{
		int indexrespawn = Random.Range(0, respawns.Length);
		if (respawns[indexrespawn].GetComponent<respawnValidator>().thing == null)
		{
			PhotonNetwork.Instantiate("playerPrefab", respawns[indexrespawn].
				transform.position, respawns[indexrespawn].transform.rotation, 0);
			InvokeRepeating("CheckStatus", 3, 1);
		}
		else
		{
			Invoke("StarGame",.1f);
		}
	}
	void CheckStatus()
	{
		tanks = FindObjectsOfType<TankId>();
		if (tanks.Length < 2)
		{
			// tanks[0]

			pview.RPC("Victory", RpcTarget.AllBuffered);
			PhotonNetwork.Instantiate("PlayerName", Vector3.zero, Quaternion.identity);
			CancelInvoke("ChechStatus");
		}
	}
	[PunRPC]
	void Victory()
	{
		tanks = FindObjectsOfType<TankId>();
		Camera.main.GetComponent<netCamera>().SetPlayer(tanks[0].gameObject);
		winner.transform.position = tanks[0].transform.position;
		winner.SetActive(true);
		Invoke("EnableGameRoom", 5);
		
	}
	void EnableGameRoom()
	{
		gameRoom.enabled = true;
	}
}
