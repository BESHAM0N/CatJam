using CatJam;
using DG.Tweening;
using UnityEngine;

public class FadeViewAnimator : IViewAnimator
{
    public void Show(CanvasGroup canvasGroup, GameObject panel, float duration)
    {
        //canvasGroup.DOKill();
        // canvasGroup.DOFade(1f, duration)
        //     .SetUpdate(true)
        //     .OnStart(() =>
        //     {
        //         canvasGroup.interactable = true;
        //         canvasGroup.blocksRaycasts = true;
        //     });
        
        if (panel != null) panel.SetActive(true);

    
    }

    public void Hide(CanvasGroup canvasGroup, GameObject panel, float duration)
    {
        // canvasGroup.DOKill();
        // canvasGroup.DOFade(0f, duration)
        //     .SetUpdate(true)
        //     .OnComplete(() => panel.SetActive(false));
        // canvasGroup.interactable = false;
        // canvasGroup.blocksRaycasts = false;

        if (panel != null) panel.SetActive(false);
    }
}