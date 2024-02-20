using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelf : MonoBehaviour
{
    public GameObject ShowInstruction;
    // Start is called before the first frame update

    // Update is called once per frame

    public void ShowInstructionText()
    {
        if (ShowInstruction != null)
        {
            ShowInstruction.SetActive(true);
        }
    }

    public void HideInstruction()
    {

        if (ShowInstruction != null)
        {
            ShowInstruction.GetComponent<Animator>().SetBool("FadeOutIns", true);
            StartCoroutine(Hide());
            IEnumerator Hide()
            {
                yield return new WaitForSeconds(2);
                ShowInstruction.SetActive(false);

            }
        }
    }
}
