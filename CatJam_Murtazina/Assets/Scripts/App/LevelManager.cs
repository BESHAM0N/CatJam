using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class LevelManager : MonoBehaviour
    {
        public event Action OnLevelLoading;
        
        private Ground _ground;
        private EntitiesView _entitiesView;
        private EntityFactory _entityFactory;
        private int _currentCatCount;
        
        private const int INITIAL_CAT_VALUE = 3;
        private const int MAX_CAT_VALUE = 8;

        [Inject]
        public void Construct(Ground ground, EntityFactory entityFactory, EntitiesView entitiesView)
        {
            _ground = ground ?? throw new ArgumentNullException(nameof(ground));
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            _entitiesView = entitiesView ?? throw new ArgumentNullException(nameof(entitiesView));
            _currentCatCount = INITIAL_CAT_VALUE;
        }

        private void Start()
        {
            _entitiesView.Initialize();
            OnLevelLoading?.Invoke();
            GenerateLevel();
        }

        private void FixedUpdate()
        {
            if (!_entitiesView.AllCatsIsHide) return;
            
            if (_currentCatCount < MAX_CAT_VALUE)
                _currentCatCount++;

            GenerateLevel();
        }

        private void GenerateLevel()
        {
            _ground.Reset();
            _entityFactory.ClearAllObjects();

            _entityFactory.CreateBorderObstacles();
            _entityFactory.GenerateCats(_currentCatCount);
            _entityFactory.GenerateObstacles();
            _entityFactory.ClearPathsForCats();

            var obstacles = _entityFactory.AllObstacles.ToArray();
            var cats = _entityFactory.CreatedCats.ToArray();

            _entitiesView.UpdateObjects(obstacles, cats);
        }
    }
}