using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : MonoBehaviour {

	[HideInInspector] public static int gearActive;
	[HideInInspector] public const int numGears = 4;
	private PlayerController player;
	private Animator anim;
	private float mf, mfa;

	public float unicycleSpeedMult; //player goes faster on unicycle

	void Awake () {
		gearActive = 0;
		player = GetComponent<PlayerController>();
		mf = player.moveForce;
		mfa = player.moveForceAir;
		anim = GetComponent<Animator>();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.U)) //UNICYCLE GEAR
			SwitchGear(1);
		else if(Input.GetKeyDown(KeyCode.I)) //ICE BLOCK GEAR
			SwitchGear(2);
		else if(Input.GetKeyDown(KeyCode.O)) //BALLOON GEAR
			SwitchGear(3);
		else if(Input.GetKeyDown(KeyCode.P)) //TELEPORT GEAR
			SwitchGear(4);
	}

	void SwitchGear (int gear) {
		Debug.Log("Switching up");
		gearActive = (gearActive == gear)? 0 : gear;

		switch (gearActive)
		{
			case 0:
				toNeutral();
				break;
			case 1:
				toUnicycle();
				break;
			case 2:
				toIceBlock();
				break;
			case 3:
				toBalloon();
				break;
			case 4:
				toTeleport();
				break;
		}
	}

	void toNeutral () {
		neutralMove();
		anim.SetTrigger("NoGear");
	}

	void toUnicycle () {
		player.moveForce = mf*unicycleSpeedMult;
		player.moveForceAir = mf*unicycleSpeedMult;
		anim.SetTrigger("Unicycle");
	}

	void toIceBlock () {
		neutralMove();
		anim.SetTrigger("Ice Block");
	}

	void toBalloon () {
		neutralMove();
		anim.SetTrigger("NoGear");
	}

	void toTeleport () {
		neutralMove();
		anim.SetTrigger("Teleport");
	}

	void neutralMove() {
		player.moveForce = mf;
		player.moveForceAir = mfa;
	}
}
