using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    private bool attack;

    private bool slide;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

	// Use this for initialization
	void Start () {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        movementSpeed = 12;
	}

    private void Update() {
        HandleInputs();
    }

    void FixedUpdate () {

        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        HandleAttacks();
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) {
            Flip(horizontal);
        }
        ResetValues();
	}

    private void HandleMovement(float horizontal) {

        if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) {
            myAnimator.SetBool("slide", true);
        } else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) {
            myAnimator.SetBool("slide", false);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks() {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleInputs() {
        if (Input.GetKeyDown(KeyCode.Period)) {
            attack = true;
        }
        if (Input.GetKeyDown(KeyCode.Comma)) {
            slide = true;
        }
    }

    private void Flip(float horizontal) {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void ResetValues() {
        attack = false;
        slide = false;
    }
}
