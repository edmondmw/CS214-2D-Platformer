using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {

    public float horizontalJumpForce;
    public Transform wallCheck;

    private bool onWall = false;
    private bool shouldJump = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private SimplePlatformController spc;

	void Awake () {
        spc = GetComponent<SimplePlatformController>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        onWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));

        if(Input.GetButtonDown("Jump") && (onWall && !spc.grounded))
        {
            shouldJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (shouldJump)
        {
            anim.SetTrigger("Jump");
            rb2d.velocity= new Vector2(0f, 0.5f * rb2d.velocity.y);
            if (spc.facingRight)
            {
                rb2d.AddForce(new Vector2(-horizontalJumpForce, spc.verticalJumpForce));
            } else
            {
                rb2d.AddForce(new Vector2(horizontalJumpForce, spc.verticalJumpForce));
            }
            shouldJump = false;
        }
       
    }
}
