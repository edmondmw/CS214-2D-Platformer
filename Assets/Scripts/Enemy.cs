using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    public float speed;
    public Transform heroTransform;

    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private bool onGround = false;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            StartCoroutine("Move");
        }
        
        //crappy but workaround
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(collision.gameObject.CompareTag("ground"))
        {
            onGround = true;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    IEnumerator Move()
    {
        float direction = Mathf.Sign(transform.position.x - heroTransform.position.x) * -1;
        rb2d.velocity = new Vector2(speed * direction, 0);
        
        if((direction > 0 && !facingRight) ||(direction < 0 && facingRight))
        {
            Flip();
        }


        if (Mathf.Round(transform.position.x) == Mathf.Round(heroTransform.position.x)) 
        {
            //Make the enemy unable to move if you have the same x position
            //Allows player to jump over them
            onGround = false;
            yield return new WaitForSeconds(1f);
            onGround = true;
        }
    }
}
