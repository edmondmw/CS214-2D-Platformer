using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteors : MonoBehaviour {

    public Transform[] meteorSpawns;
    public GameObject meteor;
    public bool spawnEnabled = true;
    public float seconds = 2f;

    private float yPos;

    private Camera cam;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn(seconds));

        cam = Camera.main;
	}

    void Update()
    {
        float camHalfVertical = cam.orthographicSize;
        yPos = cam.transform.position.y + camHalfVertical + 15;
        
        foreach(Transform aMeteor in meteorSpawns)
        {
            aMeteor.transform.position = new Vector3(aMeteor.transform.position.x, yPos, 0);
        }
    }

    IEnumerator Spawn(float seconds)
    {
        while(spawnEnabled)
        {
            yield return new WaitForSeconds(seconds);
            Instantiate(meteor, meteorSpawns[Random.Range(0, 2)].position, Quaternion.identity);
        }
    }
}
