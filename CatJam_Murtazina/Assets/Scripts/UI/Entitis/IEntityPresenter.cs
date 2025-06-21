using System;
using UnityEngine;

namespace CatJam
{
    public interface IEntitisPresenter
    {
        public event Action<Cat, Vector2Int> OnCatPositionChanged;
        
        public void UpdateState(Cat[] cat);
        public void OnDispose(Cat[] cat);
    }
}