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
    // Start is called before the first frame update

    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Debug.Log("Piper loaded");
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump")){ //&& grounded) {
            jump = true;
        }
    }
    void FixedUpdate()
    {
        float hRaw = Input.GetAxisRaw("Horizontal");
        float h = (hRaw == 0 ? hRaw : Input.GetAxis("Horizontal"));
        if (h * rb2D.velocity.x < maxSpeed)
            rb2D.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs (rb2D.velocity.x) > maxSpeed)
            rb2D.velocity = new Vector2(Mathf.Sign (rb2D.velocity.x) * maxSpeed, rb2D.velocity.y);
        
        if (jump) {
            rb2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (h == 0) {
            rb2D.velocity = new Vector2(rb2D.velocity.x * decel, rb2D.velocity.y);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stick")
        {
            Debug.Log("Yep, that's a stick");
            collision.gameObject.SendMessage("Become_Collectable");
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
}
