using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastActiveScene : MonoBehaviour
{
    public Animator Transition;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(LoadLastActive());

            IEnumerator LoadLastActive()
            {
                Transition.SetBool("Out", true);
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }


}
