using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{

    public int offset = 10;
    public bool hasTop = false;

    public int maxWalls;
    public GameObject grass;

    private float wallHeight;
    private Camera cam;
    private Transform myTransform;
    private static int wallCount = 2;


    //Use Awake for referencing between scripts
    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start()
    {
        MeshRenderer mRenderer = GetComponent<MeshRenderer>();
        wallHeight = mRenderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTop && wallCount < maxWalls)
        { 
            //orthographicSize returns half the camera's vertical length. Character isn't exactly in the center but we'll use this as an estimate
            float camHalfVertical = cam.orthographicSize;
            //
            float edgeVisiblePositionTop = (myTransform.position.y + wallHeight / 2) - camHalfVertical;

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
        Transform newWall = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;
        newWall.parent = myTransform.parent;
        wallCount++;

        if(wallCount >= maxWalls - 1)
        {
           
            Vector3 grassPosition = new Vector3(newPosition.x, newPosition.y + wallHeight/2 + .1f, 0);
            //place grass on top
            Instantiate(grass, grassPosition, Quaternion.identity);
        }
    }
}
