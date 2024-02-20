using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressZ : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Fan;
    public GameObject FanTag;
    public Animator Button;
    public GameObject FanWind;
    [SerializeField] AudioSource ButtonClicked;
    bool Pressed = false;

    private void Update()
    {
        bool isBeingPressed = Physics2D.Raycast(transform.position, Vector2.up, .1f);
        if (isBeingPressed) {
            if (ButtonClicked != null)
            {
                if (Pressed == false)
                {
                    if (!ButtonClicked.isPlaying)
                    {
                        ButtonClicked.Play();
                        Pressed = true;
                    }
                }

            }

            if (FanWind != null)
            {
                FanWind.SetActive(false);
            }
            if (Fan != null)
            {
                Fan.SetBool("FanOFF", true);
            }

            if (FanTag != null)
            {
                FanTag.tag = "Untagged";
            }

            if (Button != null)
            {
                Button.SetBool("Pressed", true);
            }
        }

        else
        {
            if (ButtonClicked != null)
            {
                if (Pressed == true) {
                    if (ButtonClicked.isPlaying)
                    {
                        ButtonClicked.Stop();
                        Pressed = false;
                    }
                }

            }

            if (FanWind != null)
            {
                FanWind.SetActive(true);
            }
            if (Fan != null)
            {
                Fan.SetBool("FanOFF", false);
            }

            if (FanTag != null)
            {
                FanTag.tag = "Obstacles";
            }

            if (Button != null)
            {
                Button.SetBool("Pressed", false);
            }
        }

    }
}
