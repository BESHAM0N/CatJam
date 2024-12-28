using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam
{
    public sealed class Ground
    {
        public int Width => _width;
        public int Height => _height;

        private readonly int _width;
        private readonly int _height;
        private readonly object[,] _cells;
        private readonly Dictionary<object, Vector2Int> _objects;

        public Ground(Vector2Int size)
        {
            _width = size.x;
            _height = size.y;

            _cells = new object[_width, _height];
            _objects = new Dictionary<object, Vector2Int>();
        }

        public object GetObject(Vector2Int position)
        {
            if (!IsWithinBounds(position)) return null;
            return _cells[position.x, position.y];
        }

        public void AddObject(object target, Vector2Int position)
        {
            if (_objects.ContainsKey(target) || !IsWithinBounds(position) || _cells[position.x, position.y] != null)
            {
                Debug.Log($"AddObject failed: Invalid target or position {position}.");
                return;
            }

            _cells[position.x, position.y] = target;
            _objects.Add(target, position);
        }

        public bool MoveObject(object target, Vector2Int position)
        {
            if (!IsWithinBounds(position))
            {
                Debug.LogError($"MoveObject failed: position {position} is out of bounds.");
                return false;
            }

            if (!_objects.TryGetValue(target, out Vector2Int prevPosition))
            {
                Debug.LogError($"MoveObject failed: target {target} not found.");
                return false;
            }

            if (_cells[position.x, position.y] != null)
            {
                Debug.LogError(
                    $"MoveObject failed: position {position} is already occupied by {_cells[position.x, position.y]}.");
                return false;
            }

            _cells[prevPosition.x, prevPosition.y] = null;
            _cells[position.x, position.y] = target;
            _objects[target] = position;
            return true;
        }

        public List<Vector2Int> GetPathToEdge(Vector2Int startPosition, DirectionType direction)
        {
            if (!IsWithinBounds(startPosition))
            {
                Debug.LogError($"GetPathToEdge failed: startPosition {startPosition} is out of bounds.");
                return new List<Vector2Int>();
            }

            List<Vector2Int> path = new();
            Vector2Int offset = GetDirectionOffset(direction);

            Vector2Int currentPosition = startPosition + offset;

            while (IsWithinBounds(currentPosition))
            {
                path.Add(currentPosition);
                currentPosition += offset;
            }

            return path;
        }

        public void RemoveObject(object target)
        {
            if (!_objects.TryGetValue(target, out Vector2Int position))
            {
                Debug.LogError($"RemoveObject failed: target {target} not found.");
                return;
            }

            _cells[position.x, position.y] = null;
            _objects.Remove(target);

            if (target is MonoBehaviour monoBehaviour)
            {
                GameObject.Destroy(monoBehaviour.gameObject);
            }
        }

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    yield return new Vector2Int(x, y);
                }
            }
        }

        public void Reset()
        {
            _objects.Clear();
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _cells[x, y] = null;
                }
            }
        }

        private bool IsWithinBounds(Vector2Int position)
        {
            return position.x >= 0 && position.x < _width && position.y >= 0 && position.y < _height;
        }

        private static Vector2Int GetDirectionOffset(DirectionType direction)
        {
            return direction switch
            {
                DirectionType.Up => new Vector2Int(0, 1),
                DirectionType.Down => new Vector2Int(0, -1),
                DirectionType.Left => new Vector2Int(-1, 0),
                DirectionType.Right => new Vector2Int(1, 0),
                _ => Vector2Int.zero
            };
        }
    }
}