using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class EntitisPresenter : IEntitisPresenter
    {
        public event Action<Cat, Vector2Int> OnCatPositionChanged;

        public void UpdateState(Cat[] cats)
        {
            foreach (var cat in cats)
            {
                cat.OnPositionChanged += position => OnCatPositionChanged.Invoke(cat, position);
            }
        }

        public void OnDispose(Cat[] cats)
        {
            foreach (var cat in cats)
            {
                cat.OnPositionChanged -= position => OnCatPositionChanged.Invoke(cat, position);
            }
        }
    }
}