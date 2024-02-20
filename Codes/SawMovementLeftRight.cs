using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public Vector3 Left = Vector3.left;
    public Vector3 Right = Vector3.right;
    public float MoveSpeed = 2f;
    bool isMovingLeft = false;
    bool isMovingRight = false;
    public float MoveDuration = 2f;

    private void Start()
    {
        isMovingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingLeft == true && isMovingRight == false) { 
            transform.position = transform.position + (Left * MoveSpeed) * Time.deltaTime;
            StartCoroutine(WaitForMove());

            IEnumerator WaitForMove()
            {
                yield return new WaitForSeconds(MoveDuration);
                isMovingLeft=false;
                isMovingRight=true;
            }
        } else if (isMovingLeft == false && isMovingRight == true) {
            transform.position = transform.position + (Right * MoveSpeed) * Time.deltaTime;
            StartCoroutine(WaitForMove());

            IEnumerator WaitForMove()
            {
                yield return new WaitForSeconds(MoveDuration);
                isMovingLeft = true;
                isMovingRight = false;
            }
        }
    }
}
