using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {
    public float parallaxSpeed;
    public float offset = 5; //A bit of a buffer to prevent errors where background doesn't load fast enough

    private Transform[] tiles;
    private Transform cam;
    private int leftIndex;
    private int rightIndex;
    private float tileWidth;
    private float lastCameraX;

	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
        lastCameraX = cam.position.x;
        //Initialize the tiles array to contain the transforms of all of the tiles
        tiles = new Transform[transform.childCount];
        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = tiles.Length - 1;
        tileWidth = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Difference in x position since the last frame
        float deltaX = cam.position.x - lastCameraX;
        transform.position += Vector3.left * (deltaX * parallaxSpeed);
        lastCameraX = cam.position.x;

        float  halfHorizontalCamera = Camera.main.orthographicSize * Screen.width / Screen.height;
        float visibleRightEdge = tiles[rightIndex].transform.position.x + tileWidth/2 - offset;
        float visibleLeftEdge = tiles[leftIndex].transform.position.x - tileWidth / 2 + offset;

        if (cam.position.x + halfHorizontalCamera > visibleRightEdge)
        {
            ScrollRight();
        }

        if(cam.position.x - halfHorizontalCamera < visibleLeftEdge)
        {
            ScrollLeft();
        }
	}

    //Move the leftmost tile to the rightmost position
    void ScrollRight ()
    {
        Vector3 rightTilePos = tiles[rightIndex].position;
        tiles[leftIndex].position = new Vector3(rightTilePos.x + tileWidth, rightTilePos.y, rightTilePos.z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex >= tiles.Length)
            leftIndex = 0;
    }

    //Move the rightmost tile to the leftmost position
    void ScrollLeft()
    {
        Vector3 leftTilePos = tiles[leftIndex].position;
        tiles[rightIndex].position = new Vector3(leftTilePos.x - tileWidth, leftTilePos.y, leftTilePos.z);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = tiles.Length - 1;
    }
}
