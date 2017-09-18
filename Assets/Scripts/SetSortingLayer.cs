using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour {
    public string sortingLayer;
    public int sortingOrder;

    private MeshRenderer rend;

	void Awake () {
        rend = GetComponent<MeshRenderer>();
        rend.sortingLayerName = sortingLayer;
        rend.sortingOrder = sortingOrder;
	}
}
