using CatJam;
using DG.Tweening;
using UnityEngine;

public class FadeViewAnimator : IViewAnimator
{
    public void Show(CanvasGroup canvasGroup, GameObject panel, float duration)
    {
        canvasGroup.DOKill();
        panel.SetActive(true);
        canvasGroup.DOFade(1f, duration)
            .SetUpdate(true)
            .OnStart(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
    }

    public void Hide(CanvasGroup canvasGroup, GameObject panel, float duration)
    {
        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, duration)
            .SetUpdate(true)
            .OnComplete(() => panel.SetActive(false));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}