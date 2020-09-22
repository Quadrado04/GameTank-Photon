using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
	PhotonView pview;
	float power;
	float turn;
	float turnAxis;
	Rigidbody rdb;
	Animator anim;
	bool run = false;
    // Start is called before the first frame update
    void Start()
    {
		rdb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		pview = GetComponent<PhotonView>();
		if (pview.IsMine)
		{
			Camera.main.GetComponent<netCamera>().SetPlayer(gameObject);
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (pview.IsMine)
		{
			power = Input.GetAxis("Vertical");
			turn = Input.GetAxis("Horizontal");
			turnAxis = Input.GetAxis("Mouse X");
			run = Input.GetButton("Fire3");
			if (Input.GetButtonDown("Fire1"))
			{
				pview.RPC("Shoot", RpcTarget.AllBuffered, null);
			}
		}
			
	}
	private void FixedUpdate()
	{
		if (pview.IsMine)
		{
			transform.Rotate(transform.up * turnAxis);
			Vector3 localmov = transform.TransformDirection(new Vector3(turn, 0, power));
			if (run)
			{
				localmov = localmov * 4;
			}
			else
			{
				localmov = localmov * 2;
			}
			rdb.velocity = new Vector3(localmov.x, rdb.velocity.y, localmov.z);
		}
			

		Vector3 localVel = transform.InverseTransformDirection(rdb.velocity)/2;
		anim.SetFloat("VelX", localVel.x);
		anim.SetFloat("VelZ", localVel.z);
	}
	[PunRPC]
	void Shoot()
	{
		anim.SetTrigger("Shoot");
	}
}
