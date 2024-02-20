using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Rigidbody2D rigidbody2;
    public float MoveSpeed = 5f;
    bool FruitJumped = false;
    public float JumpPower = 5f;
    // Update is called once per frame
    void Update()
        
    {


        if (rigidbody2 != null)
        {
            StartCoroutine(SpawnMushroom(.125f));

            IEnumerator SpawnMushroom (float Duration)
            {
                yield return new WaitForSeconds(Duration);
                if (FruitJumped == false)
                {
                    rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, JumpPower);
                    FruitJumped = true;
                }
                float MoveX = 2;
                rigidbody2.velocity = new Vector2(MoveX * MoveSpeed, rigidbody2.velocity.y);

                
            }
        }

    }
}
