using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    // Start is called before the first frame update
    public string SortingLayerName = "Default";
    public int OrderInLayer = 0;
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        gameObject.GetComponent<MeshRenderer>().sortingOrder = OrderInLayer;
    }
}
