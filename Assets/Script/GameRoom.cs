using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRoom : MonoBehaviour
{
	public GameObject panel;
	public PlayerName[] players;
    // Start is called before the first frame update
    void Start()
    {
		panel.SetActive(true);
		players =FindObjectsOfType<PlayerName>();
		foreach (PlayerName player in players)
		{
			player.Reset();
		}
		InvokeRepeating("CheckAllReady", 2, 1);
    }
	void CheckAllReady()
	{
		bool allready = false;
		if (players.Length > 1)
		{
			allready = players.All(x => x.ready);

			if (allready)
			{
				PhotonNetwork.CurrentRoom.IsOpen = false;
				PhotonNetwork.LoadLevel("Level1");
			}
		}

	}
}
