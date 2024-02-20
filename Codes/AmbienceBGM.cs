using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmbienceBGM : MonoBehaviour
{
    [SerializeField] AudioSource Ambience1;
    [SerializeField] AudioSource Ambience2;
    [SerializeField] AudioSource Ambience3;
    [SerializeField] AudioSource Ambience4;
    [SerializeField] AudioSource Ambience5;

    AmbienceBGM instance;

    void StopMusic()
    {
        if (Ambience1 != null || Ambience2 != null || Ambience3 != null || Ambience4 != null || Ambience5 != null)
        {
            if (Ambience1 != null)
            {
                if (Ambience1.isPlaying)
                {
                    Ambience1.Stop();
                }
            }            
            if (Ambience2 != null)
            {
                if (Ambience2.isPlaying)
                {
                    Ambience2.Stop();
                }
            }            
            
            if (Ambience3 != null)
            {
                if (Ambience3.isPlaying)
                {
                    Ambience3.Stop();
                }
            }            
            
            if (Ambience4 != null)
            {
                if (Ambience4.isPlaying)
                {
                    Ambience4.Stop();
                }
            }            
            
            if (Ambience5 != null)
            {
                if (Ambience5.isPlaying)
                {
                    Ambience5.Stop();
                }
            }

        }
    }
}
