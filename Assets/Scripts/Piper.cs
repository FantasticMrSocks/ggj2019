﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piper : MonoBehaviour
{

	List<GameObject> sticks;
	private Rigidbody2D rb2D;
	private bool grounded = false;
	public Transform groundCheck;
	public float jumpForce = 1000f;
	public float maxSpeed = 5f;
	public float moveForce = 365f;
	public float decel = 0.75f;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool buildMode = false;
	public Animator animator;
	public float GroundCheck = 0;

	// Start is called before the first frame update

	void Awake() {
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Piper loaded");
		animator = GetComponent<Animator>();
		animator.SetBool("Grounded", true);
	}

	// Update is called once per frame
	void Update()
	{
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if (Input.GetButtonDown("Jump") && !buildMode && grounded) {
			jump = true;
		}
		if ( !grounded){
			if (rb2D.velocity.y == 0) {
				grounded = true;
				animator.SetBool("Grounded", true);
			}

		}
	}
	void FixedUpdate()
	{
		if (!buildMode)
		{
			float hRaw = Input.GetAxisRaw("Horizontal");
			float h = (hRaw == 0 ? hRaw : Input.GetAxis("Horizontal"));
			if (h * rb2D.velocity.x < maxSpeed)
				rb2D.AddForce(Vector2.right * h * moveForce);

			if (Mathf.Abs(rb2D.velocity.x) > maxSpeed)
				rb2D.velocity = new Vector2(Mathf.Sign(rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);

			animator.SetFloat("Speed", Mathf.Abs(rb2D.velocity.x));

			if (jump)
			{
				rb2D.AddForce(new Vector2(0f, jumpForce));
				jump = false;
				animator.SetBool("Grounded", false);
				grounded = false;
			}
			if (h == 0)
			{
				rb2D.velocity = new Vector2(rb2D.velocity.x * decel, rb2D.velocity.y);
			}
			else
			{
				GetComponent<SpriteRenderer>().flipX = h < 0;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Stick")
		{
			Debug.Log("Yep, that's a stick");
			collision.gameObject.SendMessage("Become_Collectable");
		}

		if (collision.gameObject.tag == "Camera Zone") {
			Debug.Log("Entering " + collision.gameObject.name);
			UnityEngine.Camera.main.GetComponent<Camera>().currentZone = collision.gameObject;            
		}

		//if (collision.relativeVelocity.magnitude > 2)
		//    audioSource.Play();
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Stick")
		{
			collision.gameObject.SendMessage("Become_Uncollectable");
		}

		//if (collision.relativeVelocity.magnitude > 2)
		//    audioSource.Play();
	}

	void SetBuildMode(bool newValue)
	{
		buildMode = newValue;
		animator.SetBool("Build", buildMode);
	}
}