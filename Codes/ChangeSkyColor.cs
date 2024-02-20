using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyColor : MonoBehaviour
{
    public Animator CameraForSky;
    public bool AnimationBoolean;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MyTagPlayerTag")) { 
            CameraForSky.SetBool("Change", AnimationBoolean);
        }
    }
}
