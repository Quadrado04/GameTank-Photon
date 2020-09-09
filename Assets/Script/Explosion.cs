using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
	void SelfDestroy()
	{
		Destroy(gameObject, 4);
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
