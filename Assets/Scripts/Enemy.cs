using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    public float speed;
    public Transform heroTransform;

    private Rigidbody2D rb2d;
    private bool facingRight;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    void Move()
    {
        float direction = Mathf.Sign(transform.position.x - heroTransform.position.x) * -1;
        rb2d.velocity = new Vector2(speed * direction, 0);
        
        if((direction > 0 && !facingRight) ||(direction < 0 && facingRight))
        {
            Flip();
        }
    }
    
}
