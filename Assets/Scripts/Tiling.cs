using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling : MonoBehaviour
{

    public int offset = 10;
    public bool hasTop = false;
    public int maxWalls;

    private float height;
    private Camera cam;
    private Transform myTransform;

    //Use Awake for referencing between scripts
    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    void Start()
    {
        MeshRenderer mRenderer = GetComponent<MeshRenderer>();
        height = mRenderer.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTop)
        {
            //Get half of the camera length
            float camVerticalExtent = cam.orthographicSize * Screen.height / Screen.width;
            //
            float edgeVisiblePositionTop = (myTransform.position.y + height / 2) - camVerticalExtent;

            if (cam.transform.position.y >= edgeVisiblePositionTop - offset && !hasTop)
            {
                MakeNewWall();
                hasTop = true;
            }
        }
    }

    void MakeNewWall()
    {
        Vector3 newPosition = new Vector3(myTransform.position.x, myTransform.position.y + height, 0);
        Transform newWall = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;
        newWall.parent = myTransform.parent;
    }
}
