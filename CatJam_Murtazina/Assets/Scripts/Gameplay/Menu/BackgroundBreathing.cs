namespace CatJam.Menu
{
    using UnityEngine;
    using DG.Tweening;

    public class BackgroundBreathing : MonoBehaviour
    {
        [SerializeField] private float zoomInScale = 1.1f;
        [SerializeField] private float duration = 4f;
        [SerializeField] private Ease easeType = Ease.InOutSine;

        private Tween _zoomTween;

        private void Start()
        {
            _zoomTween = transform
                .DOScale(zoomInScale, duration / 2f)
                .SetEase(easeType)
                .SetLoops(-1, LoopType.Yoyo); // бесконечный цикл туда-сюда
        }

        private void OnDestroy()
        {
            _zoomTween?.Kill();
        }
    }
}