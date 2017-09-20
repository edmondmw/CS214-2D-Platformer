using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float destroyDelay = 0.5f;

    private Camera cam;
    private float offset = 2;

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject, destroyDelay);
            Destroy(gameObject, destroyDelay);
        }
    }
}
