using System;
using UnityEngine;

namespace CatJam
{
    public sealed class ObjectView : MonoBehaviour
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private SpriteRenderer _renderer;

        public void SetSprite(Sprite icon)
        {
            _renderer.sprite = icon;
        }
    }
}