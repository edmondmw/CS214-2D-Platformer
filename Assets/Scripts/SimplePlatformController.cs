using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
    [HideInInspector] public bool onWall = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float verticalJumpForce = 1000f;
    public float horizontalJumpForce;
	public Transform groundCheck;
    public Transform wallCheck;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake ()
    {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Linecast returns true if it hits something in between
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
        onWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));

        if (Input.GetButtonDown ("Jump") && (grounded || (!grounded && onWall)))
        {
			jump = true;
		}
	}

	void FixedUpdate()
    {
		float h = Input.GetAxis ("Horizontal");
		//sets animator speed
		anim.SetFloat ("Speed", Mathf.Abs (h));

        //Ground movement for walking
        if (grounded)
        {
            if (h * rb2d.velocity.x < maxSpeed)
            {
                rb2d.AddForce(Vector2.right * h * moveForce);
            }

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            {
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
            }
        }

		if ((facingRight && h < 0) || (!facingRight && h > 0))
        {
			Flip ();
		}

		if (jump) {
            anim.SetTrigger("Jump");
            if (grounded)
            {
                rb2d.AddForce(new Vector2(0f, verticalJumpForce));
            } else
            {
                if(facingRight)
                {
                    rb2d.AddForce(new Vector2(-horizontalJumpForce, verticalJumpForce));
                } else
                {
                    rb2d.AddForce(new Vector2(horizontalJumpForce, verticalJumpForce));
                }
                
            }
			
			jump = false;
		}

	}

	void Flip () { 
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
