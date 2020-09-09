using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TankId : MonoBehaviour
{
	public TextMesh name;
	public PhotonView pview;
	public int life = 100;
	// Start is called before the first frame update
	void Start()
    {
		name.text = pview.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
		//name.transform.LookAt(Camera.main.transform);
		name.transform.forward = transform.position - Camera.main.transform.position;
		if (pview.IsMine)
		{
			if (life <= 0)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}
    }
	public void DamageTaken(Vector3 pos)
	{
		if (pview.IsMine)
		{
			float distance = Vector3.Distance(pos, transform.position);

			life -= 10; // (int)(33 / (distance + 1));
			pview.RPC("DamageCall", RpcTarget.All, pos, life);
			GetComponent<Rigidbody>().AddExplosionForce(2000, pos, 20);
		}
	}
	[PunRPC]
	public void DamageCall(Vector3 pos,int lifesRemain)
	{
		life = lifesRemain;	
		name.text = pview.Owner.NickName+" "+life.ToString();
	}
}
