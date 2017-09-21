using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float destroyDelay = 0.2f;
    private Camera cam;
    private float offset = 1;
    private bool used = false;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        float cameraRightBound = cam.transform.position.x + cam.orthographicSize * Screen.width / Screen.height;
        float cameraLeftBound = cam.transform.position.x - cam.orthographicSize * Screen.width / Screen.height;

        if (transform.position.x - offset > cameraRightBound || transform.position.x + offset < cameraLeftBound)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !used)
        {
            used = true;
            //code to make the enemy fall through ground
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
            Destroy(collision.gameObject.GetComponent<CircleCollider2D>());
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 15f;

            //destroy rocket and enemy
            Destroy(collision.gameObject, destroyDelay);
            Destroy(gameObject);
        }
    }
}
