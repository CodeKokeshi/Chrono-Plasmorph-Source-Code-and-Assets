using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPlayerPrefs : MonoBehaviour
{
    bool Cleared = false;
    [SerializeField] string ThisWorldPrefs;

    // Update is called once per frame
    void Update()
    {
        // Check if the next scene is available
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        bool isNextSceneAvailable = nextSceneIndex < SceneManager.sceneCountInBuildSettings;

        // Check if the previous scene is available
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        bool isPreviousSceneAvailable = previousSceneIndex >= 0;

        int LastIndex = SceneManager.sceneCountInBuildSettings - 1;


        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z) && Cleared == false)
        {
            PlayerPrefs.DeleteKey(ThisWorldPrefs);
            Debug.Log("Cleared");
            Cleared = true;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (isNextSceneAvailable)
                {
                    if (nextSceneIndex == 0 || nextSceneIndex == LastIndex)
                    {
                        BGMLogic.instance.StopMusic();
                    }


                    // Code to handle the next scene being available
                    SceneManager.LoadScene(nextSceneIndex);
                }
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (isPreviousSceneAvailable)
                {
                    if (previousSceneIndex == 0 || nextSceneIndex == LastIndex)
                    {
                        BGMLogic.instance.StopMusic();
                    }
                    // Code to handle the previous scene being available
                    SceneManager.LoadScene(previousSceneIndex);
                }
            }
        }
    }
}
