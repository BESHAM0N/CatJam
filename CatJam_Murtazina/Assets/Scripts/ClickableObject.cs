using System;
using UnityEngine;

namespace CatJam
{
    public class ClickableObject : MonoBehaviour
    {
        public event Action OnClicked;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    HandleTap(touch.position);
                }
            }
        }

        private void HandleTap(Vector2 touchPosition)
        {
            var ray = Camera.main.ScreenPointToRay(touchPosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;
            if (hit.collider.gameObject != gameObject) return;
            
            Debug.Log("Object tapped");
            OnClicked?.Invoke();
        }

        private void OnMouseDown()
        {
            Debug.Log("Object clicked with mouse");
            OnClicked?.Invoke();
        }
    }
}