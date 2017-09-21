using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform[] spawnPoints;
    public Transform heroPos;
    public Text winText;
    public GameObject enemy;
    public int numWaves = 5;
    public float spawnWait = 1f;

    private int spawnSize = 1;
    private float groundWidth;
    private List<GameObject> enemies = new List<GameObject>();
    // Use this for initialization
    void Start () {
	    if(spawnPoints.Length > 1)
        {
            groundWidth = Mathf.Abs(spawnPoints[0].position.x - spawnPoints[1].position.x);
        }
	}
	
	// Update is called once per frame
	void Update () {
       if(spawnSize <= numWaves && enemies.Count  == 0)
        {
            StartCoroutine("SpawnEnemies");
        }

       if(spawnSize >= numWaves && enemies.Count == 0)
        {
            winText.text = "YOU WIN!";
        }

       //Inefficent
        UpdateEnemyList();
	}

    //returns the index of the ground tile the hero is currently on
    private int GetHeroTileIndex()
    {
        float lowerBound, upperBound;
        for (int i = 0; i< spawnPoints.Length; i++)
        {
             lowerBound = spawnPoints[i].position.x - groundWidth/2;
             upperBound = spawnPoints[i].position.x + groundWidth/2;
             if(heroPos.position.x >= lowerBound && heroPos.position.x <= upperBound)
             {
                 return i;
             }
        }

        //just return -1 if failes
        Debug.Log("Hero not on any tile");
        return -1;
    }


    IEnumerator SpawnEnemies()
    {
        int heroIndex = GetHeroTileIndex();
        for (int i = 0; i < spawnSize; i++)
        {
            for (int j = 0; j < spawnPoints.Length; j++)
            {
                if (j != heroIndex)
                {
                    GameObject anEnemy = Instantiate(enemy, spawnPoints[j].position, Quaternion.identity) as GameObject;
                    anEnemy.GetComponent<Enemy>().heroTransform = heroPos;
                    enemies.Add(anEnemy);
                    yield return new WaitForSeconds(spawnWait);
                    
                }
            }
        }

        spawnSize++;
    }

    //Inefficent. Need to learn how to remove enemies from list on death
    void UpdateEnemyList()
    {
        foreach(GameObject anEnemy in enemies)
        {
            if(anEnemy == null)
            {
                enemies.Remove(anEnemy);
            }
        }
    }
}
