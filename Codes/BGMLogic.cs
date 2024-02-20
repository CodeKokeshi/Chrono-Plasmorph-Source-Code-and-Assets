using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGMLogic : MonoBehaviour
{
    public static BGMLogic instance;
    [SerializeField] AudioSource audioSource;
    private bool isMusicPlaying = false;
    float InitialVolume;
    float TargetVolume;


    // Ensure only one instance exists
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Your music playback logic goes here
    public void PlayMusic(AudioClip musicClip)
    {
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true; // Set to true if you want the music to loop
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            StartCoroutine(FadeOut());
        }
        IEnumerator FadeOut() {
            float Duration = 1.6f;
            float TimeElapsed = 0;
            InitialVolume = audioSource.volume;
            TargetVolume = 0;

            while (TimeElapsed < Duration)
            {
                audioSource.volume = Mathf.Lerp(InitialVolume, TargetVolume, TimeElapsed / Duration);
                TimeElapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1.6f);
            audioSource.Stop();
            isMusicPlaying = false;
            this.Awake();
        }
    }

    // Check if music is currently playing
    public bool IsMusicPlaying()
    {
        return isMusicPlaying;
    }



    /* BGMLogic.instance.StopMusic();
     * this is how we stop the music from other scripts. */


}
