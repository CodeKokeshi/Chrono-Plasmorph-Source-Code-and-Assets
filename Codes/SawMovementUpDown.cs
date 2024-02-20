using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovementUpDown : MonoBehaviour
{
    public Vector3 Up = Vector3.up;
    public Vector3 Down = Vector3.down;
    public float MoveSpeed = 2f;
    bool isMovingUp = false;
    bool isMovingDown = false;
    public float MoveDuration = 2f;

    private void Start()
    {
        isMovingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingUp == true && isMovingDown == false)
        {
            transform.position = transform.position + (Up * MoveSpeed) * Time.deltaTime;
            StartCoroutine(WaitForMove());

            IEnumerator WaitForMove()
            {
                yield return new WaitForSeconds(MoveDuration);
                isMovingUp = false;
                isMovingDown = true;
            }
        }
        else if (isMovingUp == false && isMovingDown == true)
        {
            transform.position = transform.position + (Down * MoveSpeed) * Time.deltaTime;
            StartCoroutine(WaitForMove());

            IEnumerator WaitForMove()
            {
                yield return new WaitForSeconds(MoveDuration);
                isMovingUp = true;
                isMovingDown = false;
            }
        }
    }
}
