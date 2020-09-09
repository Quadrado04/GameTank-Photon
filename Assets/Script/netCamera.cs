using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netCamera : MonoBehaviour
{
	public GameObject player;
	public Vector3 adjust;
	public FlyCamera flycam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (player)
		{
			flycam.enabled = false;
			transform.LookAt(player.transform);
			transform.position = Vector3.Lerp(transform.position, player.transform.position - player.transform.forward*adjust.z+Vector3.up*adjust.y, Time.deltaTime);
		}
		else
		{
			flycam.enabled = true;
		}
    }
	public void SetPlayer(GameObject myplayer)
	{
		player = myplayer;
	}
}
