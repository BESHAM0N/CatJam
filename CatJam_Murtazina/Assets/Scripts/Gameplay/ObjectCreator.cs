using System.Collections.Generic;
using UnityEngine;

namespace CatJam
{
    public sealed class GameObjectCreator
    {
        public List<GameObject> Cats => _cats;
        public Dictionary<Cat, GameObject> CatsDictionary = new ();
        
        private readonly Transform _parentTransform;
        private readonly VisualObject _visualObjectPrefab;
        private List<GameObject> _cats = new ();

        public GameObjectCreator(Transform parentTransform, VisualObject visualObjectPrefab)
        {
            _parentTransform = parentTransform;
            _visualObjectPrefab = visualObjectPrefab;
        }

        public void CreateObject(Vector2Int position, Sprite icon, string name)
        {
            var scenePosition = new Vector3(position.x, position.y);
            var view = Object.Instantiate(_visualObjectPrefab, _parentTransform);
            view.SetSprite(icon);
            view.SetName(name);
            view.SetPosition(scenePosition);
        }

        public void CreateCatObject(Cat cat, Sprite icon, string name)
        {
            var scenePosition = new Vector3(cat.Position.x, cat.Position.y);
            var view = Object.Instantiate(_visualObjectPrefab, _parentTransform);
            view.SetSprite(icon);
            view.SetPosition(scenePosition);
            view.SetName(name);
            view.SetRotation(GetRotationForDirection(cat.Direction));
            CatsDictionary.Add(cat, view.gameObject);
            _cats.Add(view.gameObject);
          
            var clickable = view.gameObject.AddComponent<ClickableObject>();
            clickable.OnClicked += cat.MoveToExit;
        }
        
        public GameObject GetCat(Cat cat)
        {
            return CatsDictionary[cat];
        }
        
        public void ClearCatsList()
        {
            _cats.Clear();
            CatsDictionary.Clear();
        }

        private Quaternion GetRotationForDirection(DirectionType direction)
        {
            return direction switch
            {
                DirectionType.Up => Quaternion.Euler(0, 0, 90),
                DirectionType.Down => Quaternion.Euler(0, 0, -90),
                DirectionType.Left => Quaternion.Euler(0, -180, 0),
                DirectionType.Right => Quaternion.identity,
                _ => Quaternion.identity
            };
        }
    }
}