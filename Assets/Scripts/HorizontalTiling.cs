using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTiling : MonoBehaviour {
    //In the future just make one tiling script to handle left and right tiles. Also make more flexible to accomodate various types
    public bool hasRightTile = false;
    public bool hasLeftTile = false;
    public int offset = 2;

    private Camera cam;
    private GameObject tile;
    private float width;
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
        cam = Camera.main;
    }


    // Use this for initialization
    void Start () {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        float halfCamHorizontal = cam.orthographicSize * Screen.width / Screen.height;
        //position at which the character can see the right edge 
        float rightEdgeVisible = myTransform.position.x + width/2 - halfCamHorizontal - offset;
        //position at which the character can see the left edge
        float leftEdgeVisible = myTransform.position.x - width / 2 + halfCamHorizontal + offset;
        int left = -1, right = 1;

        if(cam.transform.position.x >= rightEdgeVisible && !hasRightTile)
        {
            //generate right neighbor
            makeNeighbor(right);
            hasRightTile = true;
        }

        if(cam.transform.position.x <= leftEdgeVisible && !hasLeftTile)
        {
            //generate left neighbor
            makeNeighbor(left);
            hasLeftTile = true;
        } 
   	}

    //-1 for left
    //1 for right
    void makeNeighbor(int direction)
    {
        Vector3 newPosition = new Vector3(myTransform.position.x + direction * width, myTransform.position.y, myTransform.position.z);
        Transform newNeighbor = Instantiate(myTransform, newPosition, Quaternion.identity) as Transform;

        if (direction < 0)
        {
            newNeighbor.GetComponent<HorizontalTiling>().hasRightTile = true;
        } else
        {
            newNeighbor.GetComponent<HorizontalTiling>().hasLeftTile = true;
        }
        
    }
    
}
