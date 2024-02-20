using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostArrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator ArrowAnim;
    public string Trigger;
    public float Duration = .233f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ArrowAnim.SetBool(Trigger, true);
        StartCoroutine(WaitForAnim());
        IEnumerator WaitForAnim()
        {
            yield return new WaitForSeconds(Duration);
            ArrowAnim.SetBool(Trigger, false);

        }
    }
}
