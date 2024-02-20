using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        // Only update the x-coordinate of the light's position
        Vector3 newPosition = transform.position;
        newPosition.x = Player.position.x;
        transform.position = newPosition;
    }
}
