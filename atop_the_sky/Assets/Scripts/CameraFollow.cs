using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public float smoothSpeed;
	public Vector3 offset;
	public Vector3 velocity = Vector3.one;

	public float YMax, YMin, XMax, XMin;
	private bool YMaxReached, YMinReached, XMaxReached, XMinReached;

	void LateUpdate () 
	{
		Vector3 goToPosition = target.position + offset;

		XMaxReached = (goToPosition.x > XMax); //XMaxReached is true if xposition>max, false otherwise
		if (!XMaxReached) //don't bother checking for XMin if we're at XMax
			XMinReached = (goToPosition.x < XMin); //XMinReached is true if xposition<min, false otherwise
		YMaxReached = (goToPosition.y > YMax); //YMaxReached is true if yposition>max, false otherwise
		if (!YMaxReached) //don't bother checking for YMin if we're at YMax
			YMinReached = (goToPosition.y < YMin); //YMinReached is true if yposition<min, false otherwise

		//if any of the position bools are true, those variables should be used instead of the target position
		goToPosition = new Vector3((XMaxReached ? XMax : (XMinReached ? XMin : goToPosition.x)), (YMaxReached ? YMax : (YMinReached ? YMin : goToPosition.y)), goToPosition.z);
		transform.position = Vector3.SmoothDamp(transform.position, goToPosition, ref velocity, smoothSpeed);
	}
}
