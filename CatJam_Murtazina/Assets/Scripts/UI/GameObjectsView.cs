using System.Linq;
using UnityEngine;

namespace CatJam
{
    public sealed class GameObjectsView : MonoBehaviour
    {
        [SerializeField] private VisualObject _objectPrefab;
        [SerializeField] private Sprite _stoneIcon;
        [SerializeField] private Sprite _catIcon;

        private GameObjectCreator _creator;

        public void Initialize()
        {
            _creator = new GameObjectCreator(transform, _objectPrefab);
        }

        public void UpdateObjects(Obstacle[] obstacles, Cat[] cats)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
          
            DisplayObjects(obstacles, _stoneIcon, "Stone");
            DisplayObjects(cats, _catIcon, cat => $"Cat {cat.Direction} {cat.Rank}");
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

        public bool AllCatsHide()
        {
            var cats = _creator.Cats;

            if (cats.All(x => !x.gameObject.activeSelf))
            {
                _creator.ClearCatsList();
                return true;
            }
            
            return false;
        }
    }
}
