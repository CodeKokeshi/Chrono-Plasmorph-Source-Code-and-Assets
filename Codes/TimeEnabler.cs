using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEnabler : MonoBehaviour
{
    Vector3 StartPosition;
    public float AddedX = 5f;
    bool TimeStarted = false;
    public GameObject Portal;
    public bool isY;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the player
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeStarted)
        {
            // Check if the player has moved AddedX units from the initial position
            if (isY == false)
            {
                if (transform.position.x >= StartPosition.x + AddedX)
                {
                    Portal.SetActive(true);
                    TimeStarted = true;
                }
            }
            else
            {
                if (transform.position.y >= StartPosition.y + AddedX)
                {
                    Portal.SetActive(true);
                    TimeStarted = true;
                }
            }

            if (TimeStarted)
            {
                // Disable this script once the time has started
                enabled = false;
            }
        }
    }
}
