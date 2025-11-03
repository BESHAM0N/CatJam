using CatJam;
using DG.Tweening;
using UnityEngine;

public class FadeViewAnimator : IViewAnimator
{
    public void Show(CanvasGroup canvasGroup, GameObject panel, float duration)
    {
        if (panel != null) 
            panel.SetActive(true);
    }

    public void Hide(CanvasGroup canvasGroup, GameObject panel, float duration)
    {
        if (panel != null) 
            panel.SetActive(false);
    }
}