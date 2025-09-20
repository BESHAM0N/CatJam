using UnityEngine;

namespace CatJam
{
    public interface  IViewAnimator
    {
        void Show(CanvasGroup canvasGroup, GameObject panel, float duration);
        void Hide(CanvasGroup canvasGroup, GameObject panel, float duration);
    }
}