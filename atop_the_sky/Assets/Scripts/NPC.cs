using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public int health;
	public float knockbackForce;
	private Rigidbody2D enemyRB;
	private SpriteRenderer enemyRend;
	public BoxCollider2D body;

	public void Awake(){
		enemyRB = GetComponent<Rigidbody2D>();
		enemyRend = GetComponent<SpriteRenderer>();
	}

	public void TakeDamage(int damage)
	{
		Debug.Log("Taking Damage");
		health -= damage;
		if (health <= 0)
			Die();
//		Debug.Log("At " + health + " health.");

		enemyRB.AddForce(Vector2.right * knockbackForce);
		Debug.Log("Force Applied");
		StartCoroutine("DamageDisplay");

	}

	public void Die()
	{
		Destroy(gameObject);
	}

	public IEnumerator DamageDisplay()
	{
		Color tmp = enemyRend.color;
		yield return new WaitForSeconds(0.17f);
		for (int i = 0; i < 3; i++){
//			enemyRend.color = Color.red;
			tmp.a = 0f;
			enemyRend.color = tmp;
			yield return new WaitForSeconds(0.17f);
//			enemyRend.color = Color.white;
			tmp.a = 1f;
			enemyRend.color = tmp;
			yield return new WaitForSeconds(0.17f);
		}

	}
}
