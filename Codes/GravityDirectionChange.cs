using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDirectionChange : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    bool OnTop;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.gravityScale *= -1;
            Rotation();
        }

    }

    void Rotation()
    {
        OnTop = !OnTop; // Update OnTop first

        if (OnTop == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
