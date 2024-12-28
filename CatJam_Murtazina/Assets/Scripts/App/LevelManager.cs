using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class LevelManager : MonoBehaviour
    {
        private Ground _ground;
        private GameObjectsView _gameObjectsView;
        private EntityFactory _entityFactory;
        private int _currentCatCount;
        
        private const int INITIAL_CAT_VALUE = 3;
        private const int MAX_CAT_VALUE = 8;

        [Inject]
        public void Construct(Ground ground, EntityFactory entityFactory, GameObjectsView gameObjectsView)
        {
            _ground = ground ?? throw new ArgumentNullException(nameof(ground));
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            _gameObjectsView = gameObjectsView ?? throw new ArgumentNullException(nameof(gameObjectsView));
            _currentCatCount = INITIAL_CAT_VALUE;
        }

        private void Start()
        {
            _gameObjectsView.Initialize();
            GenerateLevel();
        }

        private void FixedUpdate()
        {
            if (!_gameObjectsView.AllCatsHide()) return;
            
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

            var cats = _entityFactory.CreatedCats.ToArray();
            var obstacles = _entityFactory.AllObstacles.ToArray();

            _gameObjectsView.UpdateObjects(obstacles, cats);
        }
    }
}