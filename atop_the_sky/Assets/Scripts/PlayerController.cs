using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool facingRight;
	[HideInInspector] public bool jumping;
	[HideInInspector] public bool falling;
	[HideInInspector] public bool sliding;
	[HideInInspector] public float time_idle;

	public LayerMask groundLayer;

	public float moveForce; //acceleration
	public float moveForceAir; //mid air acceleration
	public float maxSpeed; //max speed
	public float jumpForce; //jump height (which is also influenced by gravity)
	public float slideForce;
	public Transform groundCheck; //line cast from this transform to check for ground
	public float fallingMultiplier; //effect of gravity on player; used to increase fall speed
	public float idleTimeToChange; //time to wait before triggering another idle animation

	private bool grounded;
	private Animator anim;
	private AnimatorStateInfo animState;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () 
	{
		grounded = false;
		facingRight = true;
		jumping = false;
		falling = false;

		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		animState = anim.GetCurrentAnimatorStateInfo(0);

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer.value); //credz to GameDevHQ
		anim.SetBool("Grounded", grounded);

		if (grounded)
		{
			//jumping
			if (Input.GetButtonDown("Jump") && canJump())
				anim.SetTrigger("Jump");

			//sliding
			else if (Input.GetButtonDown("Slide") && isIdle())
				rb2d.AddForce(Vector2.right * slideForce * (facingRight? 1 : -1));
		}
		else
		{
			//adjust gravity during fall (faster or slower depending on value of 'fallingMultiplier')
			if (rb2d.velocity.y < 0f)
			{
				rb2d.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime * fallingMultiplier;

				//accounts for e.g. running off ledges
				if (!(isAnim("Player_FallStart")||isAnim("Player_Fall")))
					anim.Play("Player_FallStart");
			}
		}
	}

	void Jump ()
	{
		rb2d.AddForce(new Vector2(0f, jumpForce));
	}

	bool canJump ()
	{
		return (isIdle()||isAnim("Player_Run")||isAnim("Player_Attack"));
	}

	bool isIdle ()
	{
		return (isAnim("Player_Idle1")||isAnim("Player_Idle2")||isAnim("Player_Idle3"));
	}

	void FixedUpdate() 
	{
		float h = Input.GetAxis("Horizontal");
		float m = (sliding? 0f : (grounded? moveForce : moveForceAir));

		anim.SetFloat("Speed", Mathf.Abs(h)); 
		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * h * m);

		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if ((h > 0 && !facingRight)||(h < 0 && facingRight))
			Flip();
	}

	void LateUpdate()
	{
		// check if player is idle by checking their current animator state
		if (isAnim("Player_Idle1"))
			time_idle += Time.deltaTime;
		else
			time_idle = 0;

		// if idle for long enough, play another animation (randomly)
		if (time_idle > idleTimeToChange)
			anim.Play((Random.value<0.5f) ? "Player_Idle2" : "Player_Idle3");
	}

	void Flip() 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isAnim(string state)
	{
		return animState.IsName(state);
	}
}
