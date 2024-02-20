using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkedOrNot : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Bulked = false;
    public bool OrangeEaten = false;

    private void Start()
    {
        Bulked = false;
        OrangeEaten = false;
    }
}
