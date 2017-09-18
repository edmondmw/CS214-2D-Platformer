using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{

    public int offset = 10; //offset to avoid bugs
    public bool hasTop = false;
    public int maxWalls;
    public GameObject grass;
    public GameObject meteor;

    private float wallHeight;
    private Camera cam;
    private Transform myTransform;
    private static int wallCount = 1;
    private bool spawnEnabled = true;


    //Use Awake for referencing between scripts
    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start()
    {
        //Get the height a single wall, which is a child of the walls object
        MeshRenderer mRenderer = this.transform.GetChild(0).GetComponent<MeshRenderer>();
        wallHeight = mRenderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTop && wallCount < maxWalls)
        { 
            //orthographicSize returns half the camera's vertical length. Character isn't exactly in the center but we'll use this as an estimate
            float camHalfVertical = cam.orthographicSize;
            //The position at which the player would be able to see the topmost edge of the walls
            float edgeVisiblePositionTop = (myTransform.position.y + wallHeight) - camHalfVertical;

            //Generate new walls when camera gets close enough to the top edge
            if (cam.transform.position.y >= edgeVisiblePositionTop - offset && !hasTop)
            {
                MakeNewWall();
                hasTop = true;
            }
        }
    }

    void MakeNewWall()
    {
        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + wallHeight, 0);
        Transform newWalls = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;
        wallCount++;
        //no longer want to spawn meteors from old meteor set. A better way to do this would be to lock spawns yPosto top of screen and then have a trigger disable them when we reach the top
        GetComponent<SpawnMeteors>().spawnEnabled = false;

        //If we are at the last set of walls, need to generate the grass on the top.
        if (wallCount == maxWalls)
        {
            //place grass on the top 2 walls
            Vector3 rightGrassPosition = new Vector3(newWalls.GetChild(0).position.x, newWalls.GetChild(0).position.y + wallHeight/2 + .1f, 0);
            Vector3 leftGrassPosition = new Vector3(newWalls.GetChild(1).position.x, newWalls.GetChild(0).position.y + wallHeight / 2 + .1f, 0);
            Instantiate(grass, rightGrassPosition, Quaternion.identity);
            Instantiate(grass, leftGrassPosition, Quaternion.identity);
        }
    }
}
