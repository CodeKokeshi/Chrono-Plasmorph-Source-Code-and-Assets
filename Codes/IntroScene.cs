using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IntroScene : MonoBehaviour
{
    public Animator GameTitle;
    public Light2D Light;
    public float Duration = 2f;
    bool DoneLightingUp = false;
    public float InitialLightSize = 2f;
    public float FinalLightSize = 25f;
    bool DoneShowingTitle = false;
    public GameObject Player;

    private void Update()
    {
        if (Light != null)
        {
            if (DoneLightingUp == false) { 
                StartCoroutine(LightenUp());

                IEnumerator LightenUp()
                {
                    float InitialIntensity = InitialLightSize;
                    float FinalIntensity = FinalLightSize;

                    float timeElapsed = 0;
                    while (timeElapsed < Duration)
                    {
                        Light.shapeLightFalloffSize = Mathf.Lerp(InitialIntensity, FinalIntensity, timeElapsed / Duration);
                        timeElapsed += Time.deltaTime;
                        yield return null;
                    }
                    DoneLightingUp = true;

                }
            }
            
            if (DoneLightingUp == true && DoneShowingTitle == false) {
                if (GameTitle != null)
                {
                    StartCoroutine(ShowTitle());
                    DoneShowingTitle = true;
                    IEnumerator ShowTitle()
                    {
                        GameTitle.SetBool("ShowGameTitle", true);

                        yield return new WaitForSeconds(.5f);
                    }
                }
            }

        }
    }
}
