using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool shouldJump = false;
    [HideInInspector] public bool grounded = false;

    public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float verticalJumpForce = 1000f;
	public Transform groundCheck;

	private Animator anim;
	private Rigidbody2D rb2d;
    private bool canWallJump = false;

	// Use this for initialization
	void Awake ()
    {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
        if (GetComponent<WallJump>() != null)
        {
            canWallJump = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Linecast returns true if it hits something in between
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

        if (Input.GetButtonDown ("Jump") && grounded)
        {
			shouldJump = true;
		}

        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
        }
	}

	void FixedUpdate()
    {
		float h = Input.GetAxis ("Horizontal");
		//sets animator speed
		anim.SetFloat ("Speed", Mathf.Abs (h));

        //If we can wall jump we don't want to be able to control horizontal movement in the air
        if(canWallJump)
        {
            if (grounded)
            {
                MoveHandler(h);
            }
        } else
        {
            MoveHandler(h);
        }
            
		if ((facingRight && h < 0) || (!facingRight && h > 0))
        {
			Flip ();
		}

		if (shouldJump) {
            Jump();
		}

	}

	void Flip () { 
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void Jump()
    {
        anim.SetTrigger("Jump");
        rb2d.AddForce(new Vector2(0f, verticalJumpForce));
        shouldJump = false;
    }

    //Input the value returned from Input.GetAxis("Horizontal") to determine how much the character should move on the horizontal axis
    void MoveHandler(float h)
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
}
