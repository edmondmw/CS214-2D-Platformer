using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectsOnGround : MonoBehaviour {
    List<Transform> objectsOnTile = new List<Transform>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectsOnTile.Add(collision.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        objectsOnTile.Remove(collision.gameObject.transform);
    }

    public List<Transform> getObjectsOnTile()
    {
        return objectsOnTile;
    }

    public void removeObject(Transform obj)
    {
        objectsOnTile.Remove(obj);
    }
}
