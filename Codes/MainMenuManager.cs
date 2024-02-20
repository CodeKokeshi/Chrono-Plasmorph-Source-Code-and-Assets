using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject LevelSelector; // We use this gameobject to check if self is active.
    public Animator Transition;

    public GameObject Loading;

    public AudioSource BGM; // We will fade this bgm
    float InitialVolume;
    float TargetVolume;

    public RectTransform LevelSelectorPanel;
    [SerializeField] float AppearPosition, OutPosition;
    [SerializeField] float TweenDuration;


    public void PlayButton()
    {
        if (LevelSelector != null)
        {
            if (!LevelSelector.activeSelf)
            {
                LevelSelector.SetActive(true);
                LevelSelectorAppear();
            }
            else
            {
                LevelSelectDisappearPublicScript();
            }
        }
    }

    public void OptionsButton()
    {
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void LevelSelection ( int buildIndex)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Loading.SetActive(true);
        StartCoroutine(LoadLevel());

        IEnumerator LoadLevel()
        {
            float Duration = 1f;
            float TimeElapsed = 0;
            InitialVolume = BGM.volume;
            TargetVolume = 0;

            while (TimeElapsed < Duration)
            {
                BGM.volume = Mathf.Lerp(InitialVolume, TargetVolume, TimeElapsed / Duration);
                TimeElapsed += Time.deltaTime;
                yield return null;
            }

            Transition.SetBool("Out", true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync(buildIndex);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Loading.SetActive(false);
        }

    }

    // This will handle the cancel button to exit the level selector
    public void Update()
    {
        
        if (LevelSelector.activeSelf)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                LevelSelectDisappearPublicScript();
            }
        }
    }

    public void LevelSelectAppearPublicScript()
    {
        LevelSelectorAppear();
    }

    public async void LevelSelectDisappearPublicScript()
    {
        await LevelSelectorDisappear();
        LevelSelector.SetActive(false);
    }

    async void LevelSelectorAppear()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        await LevelSelectorPanel.DOAnchorPosY(AppearPosition, TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    async Task LevelSelectorDisappear()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        await LevelSelectorPanel.DOAnchorPosY(OutPosition, TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
