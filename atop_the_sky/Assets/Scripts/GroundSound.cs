using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSound : MonoBehaviour {

	public AudioSource landNoise;

	void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.tag == "Player")
			landNoise.Play();
	}
}	
