using System;
using UnityEngine;

namespace CatJam
{
    public sealed class VisualObject : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _renderer;

        public void SetSprite(Sprite icon)
        {
            _renderer.sprite = icon;
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }

        public void SetName(string name)
        {
            gameObject.name = name;
        }
    }
}