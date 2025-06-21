using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class ScoreIncreaseObserver : IInitializable, IDisposable
    {
        private readonly IScore _score;
        private readonly EntitiesView _entitiesView;

        public ScoreIncreaseObserver(IScore score, EntitiesView entitiesView)
        {
            _score = score ?? throw new ArgumentNullException(nameof(score));
            _entitiesView = entitiesView ?? throw new ArgumentNullException(nameof(entitiesView));
        }

        public void Initialize()
        {
            _entitiesView.OnCatHide += OnCoinPickedUp;
        }

        public void Dispose()
        {
            _entitiesView.OnCatHide -= OnCoinPickedUp;
        }

        private void OnCoinPickedUp()
        {
            _score.AddScore();
        }
    }
}