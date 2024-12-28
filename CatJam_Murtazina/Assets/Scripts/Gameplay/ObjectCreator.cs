using System.Collections.Generic;
using UnityEngine;

namespace CatJam
{
    public sealed class GameObjectCreator
    {
        public List<GameObject> Cats => _cats;
        
        private readonly Transform _parentTransform;
        private readonly ObjectView _objectPrefab;
        private List<GameObject> _cats = new ();

        public GameObjectCreator(Transform parentTransform, ObjectView objectPrefab)
        {
            _parentTransform = parentTransform;
            _objectPrefab = objectPrefab;
        }

        public void CreateObject(Vector2Int position, Sprite icon, string name)
        {
            var scenePosition = new Vector3(position.x, position.y);
            var view = Object.Instantiate(_objectPrefab, scenePosition, Quaternion.identity, _parentTransform);
            view.SetSprite(icon);
            view.name = name;
        }

        public void CreateCatObject(Cat cat, Sprite icon, string name)
        {
            var scenePosition = new Vector3(cat.Position.x, cat.Position.y);
            var view = Object.Instantiate(_objectPrefab, scenePosition, Quaternion.identity, _parentTransform);
            view.SetSprite(icon);
            view.name = name;
            _cats.Add(view.gameObject);

            view.transform.rotation = GetRotationForDirection(cat.Direction);
          
            var clickable = view.gameObject.AddComponent<ClickableObject>();
            clickable.OnClicked += cat.MoveToExit;
           
            var catView = view.gameObject.AddComponent<CatView>();
            catView.Construct(cat);
        }
        
        public void ClearCatsList()
        {
            _cats.Clear();
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