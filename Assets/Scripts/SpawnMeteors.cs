using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteors : MonoBehaviour {

    public Transform[] meteorSpawns;
    public GameObject meteor;
    public bool spawnEnabled = true;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 0f, 3f);	
	}
	
    void Spawn()
    {
        if(spawnEnabled)
        {
            Instantiate(meteor, meteorSpawns[Random.Range(0, 2)].position, Quaternion.identity);
        }
    }
}
