using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject rocket;
    public Transform hero;
    public float speed;
    public float fireRate = 0.5f;

    private float nextFire = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            hero.GetComponent<Animator>().SetTrigger("Shoot");

            if (hero.GetComponent<SimplePlatformController>().facingRight)
            {
                GameObject aRocket = Instantiate(rocket, hero.FindChild("Gun").transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                aRocket.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            } else
            {
                GameObject aRocket = Instantiate(rocket, hero.FindChild("Gun").transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                aRocket.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            }

            nextFire = Time.time + fireRate;
        }
	}
}
