using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour {

	public Transform target;
	[HideInInspector] public float camDiff;
	[HideInInspector] public float initial;
	public float scale;

	void Awake () {
		//record initial position between main camera
		camDiff = target.position.y;
		initial = transform.position.y;
	}

	void LateUpdate () {
		//every frame, ensure camDiff is maintained
		transform.position = new Vector3(transform.position.x, initial - (target.position.y-camDiff)*scale, transform.position.z);
	}
}
