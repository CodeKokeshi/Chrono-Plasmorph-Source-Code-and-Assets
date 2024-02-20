using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public RectTransform MenuPanel;
    [SerializeField] float OutPosition, AppearPosition;
    [SerializeField] float TweenDuration;
    // Start is called before the first frame update
    void Start()
    {
        Appear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Appear()
    {
        MenuPanel.DOAnchorPosY(AppearPosition, TweenDuration).SetUpdate(true);
    }

    async Task DisappearScript()
    {
        await MenuPanel.DOAnchorPosY(OutPosition, TweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
