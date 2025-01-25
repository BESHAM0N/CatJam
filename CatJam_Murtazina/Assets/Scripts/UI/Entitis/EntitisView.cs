using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace CatJam
{
    public sealed class EntitiesView : MonoBehaviour
    {
        public event Action OnCatHide; 
        public bool AllCatsIsHide => HideAllCats();
        
        [SerializeField] private VisualObject _visualObjectPrefab;
        [SerializeField] private Sprite _obstacleIcon;
        [SerializeField] private Sprite _catIcon;
        private GameObjectCreator _creator;
        
        private const string OBSTACLES_NAME = "LOCK";
        private const string CATS_NAME = "CAT";
        private const float SPEED = 5f;
        private const float HIDE_DURATION = 0.3f;
        
        [Inject]
        private IEntitisPresenter _entitisPresenter;
        
        public void Initialize()
        {
            _creator = new GameObjectCreator(transform, _visualObjectPrefab);
            _entitisPresenter.OnCatPositionChanged += MoveCat;
        }
        
        public void UpdateObjects(Obstacle[] obstacles, Cat[] cats)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
          
            DisplayObjects(obstacles, _obstacleIcon, OBSTACLES_NAME);
            DisplayObjects(cats, _catIcon, cat => $"{CATS_NAME}: {cat.Direction} {cat.Rank}");
            _entitisPresenter.UpdateState(cats);
        }
        
        private void DisplayObjects(Obstacle[] obstacles, Sprite icon, string name)
        {
            foreach (var obstacle in obstacles)
            {
                _creator.CreateObject(obstacle.Position, icon, name);
            }
        }
        
        private void DisplayObjects(Cat[] cats, Sprite icon, System.Func<Cat, string> nameFunc)
        {
            foreach (var cat in cats)
            {
                _creator.CreateCatObject(cat, icon, nameFunc(cat));
            }
        }
        
        public void MoveCat(Cat cat, Vector2Int targetPosition)
        {
            var target = new Vector3(targetPosition.x, targetPosition.y, 0);
            var catObject = _creator.GetCat(cat);

            if (catObject == null) return;
           
            catObject.transform.DOKill();
            
            var duration = Vector3.Distance(catObject.transform.position, target) / SPEED;
            
            catObject.transform.DOMove(target, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => HideCat(catObject));
        }

        private void HideCat(GameObject catObject)
        {
            if (catObject == null) return;
           
            catObject.transform.DOKill();
          
            catObject.transform.DOScale(Vector3.zero, HIDE_DURATION)
                .SetEase(Ease.InBack) // Анимация с эффектом "отскока"
                .OnComplete(() => catObject.SetActive(false)); // После завершения деактивировать объект
            
            OnCatHide.Invoke();
        }

        private bool HideAllCats()
        {
            var cats = _creator.Cats;

            if (cats.All(x => !x.gameObject.activeSelf))
            {
                _creator.ClearCatsList();
                _entitisPresenter.OnDispose(_creator.CatsDictionary.Keys.ToArray());
                return true;
            }

            return false;
        }
    }
}
