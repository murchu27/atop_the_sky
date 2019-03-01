using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//enum Gears
//{
//	"NONE",
//	"UNICYCLE",
//	"ICE BLOCK",
//	"BALLOON"
//}

public class GearDisplay : MonoBehaviour {

	private string[] gearNames = new string[GearManager.numGears + 1] {"NONE", "UNICYCLE", "ICE BLOCK", "BALLOON", "TELEPORT"};
	public Text gearText;

	void Update () {
		gearText.text = "GEAR: " + gearNames[GearManager.gearActive];
	}
}
