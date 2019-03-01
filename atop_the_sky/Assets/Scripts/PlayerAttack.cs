using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private float timeSinceAttack;
	public float timeBtwAttack;

	public int damage;
	public Transform attackPos;
	public float attackRange;
	public LayerMask damageLayers;

	private List<GameObject> damaged;
	private Animator anim;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		damaged = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timeSinceAttack <=0)
		{	//attack
			if (Input.GetButtonDown("Attack"))
				StartCoroutine("Attack");
		}
		else 
			timeSinceAttack -= Time.deltaTime;
	}

	void OnDrawGizmosSelected () 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPos.position, attackRange);
	}

	IEnumerator Attack ()
	{
		damaged.Clear();
		anim.SetTrigger("Attack");
		yield return new WaitForSeconds(0.05f); //ensure attack animation has started

		GameObject g;
		while (GetComponent<PlayerController>().isAnim("Player_Attack"))
		{
			Collider2D[] toDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, damageLayers);
			foreach (Collider2D coll in toDamage)
			{
				g = coll.gameObject;
				if (coll.isTrigger || damaged.Contains(g))
					continue;
				g.GetComponent<NPC>().TakeDamage(damage);
				damaged.Add(g);
			}
			yield return null;
		}
		timeSinceAttack = timeBtwAttack;
	}
}
