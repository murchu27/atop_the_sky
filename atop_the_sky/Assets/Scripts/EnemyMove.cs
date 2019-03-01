using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public float enMoveForce;
	
	private IEnumerator followRoutine;
	private Rigidbody2D enemyRB;
	private Vector3 dirToPlayer;
//	private bool following;

	void Awake () {
		enemyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
		{
			followRoutine = Target(other.gameObject);
			StartCoroutine(followRoutine);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player"){
			StopCoroutine(followRoutine);
//			following = false;
		}

		if (enemyRB.velocity.sqrMagnitude > 0)
			StartCoroutine("StopTarget");
	}

	IEnumerator Target(GameObject target)
	{
		Transform targTransform = target.transform;
//		following = true;

		while (true)
		{
			dirToPlayer = targTransform.position - transform.position;
			enemyRB.AddForce(dirToPlayer.normalized * enMoveForce);

			yield return null;
		}
	}

	IEnumerator StopTarget()
	{
		while (enemyRB.velocity.sqrMagnitude > 0){
			//Debug.Log("Slow down");
			yield return null;
		}
	}
}
