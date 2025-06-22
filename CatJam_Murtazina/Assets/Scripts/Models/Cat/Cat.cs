using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam
{
    public class Cat : IEntity, ICat
    {
        public event Action<Vector2Int> OnPositionChanged;
        public event Action OnExit;
        public event Action OnDontExit;
        
        public RankType Rank { get; set; }
        public DirectionType Direction { get; set;  }
        public Vector2Int Position { get; set;  }
        public bool IsMoveable { get; set; }
        
        private readonly Ground _ground;

        public Cat(RankType rank, DirectionType direction, Vector2Int position, bool isMoveable, Ground ground)
        {
            Rank = rank;
            Direction = direction;
            Position = position;
            IsMoveable = isMoveable;
            _ground = ground ?? throw new ArgumentNullException(nameof(ground));
            _ground.AddObject(this, position);
        }

       public void MoveToExit()
        {
            if (TryMoveToPath(Direction, out Vector2Int exitPosition) ||
                TryMoveToPath(GetOppositeDirection(Direction), out exitPosition))
            {
                ExitMap(exitPosition);
                OnExit?.Invoke();
            }
            else
            {
                OnDontExit?.Invoke();
            }
        }

        private bool TryMoveToPath(DirectionType direction, out Vector2Int exitPosition)
        {
            var path = _ground.GetPathToEdge(Position, direction);

            if (IsPathBlocked(path, out exitPosition))
            {
                Debug.Log($"Path to exit in direction {direction} is blocked by another cat at {exitPosition}.");
                return false;
            }

            if (path.Count > 0 && _ground.MoveObject(this, path[^1]))
            {
                exitPosition = path[^1];
                return true;
            }

            exitPosition = default;
            return false;
        }

        private bool IsPathBlocked(IReadOnlyList<Vector2Int> path, out Vector2Int blockingPosition)
        {
            foreach (var position in path)
            {
                if (_ground.GetObject(position) is Cat)
                {
                    blockingPosition = position;
                    return true;
                }
            }

            blockingPosition = default;
            return false;
        }

        private void ExitMap(Vector2Int exitPosition)
        {
            Position = exitPosition;
            OnPositionChanged?.Invoke(Position);
            _ground.RemoveObject(this);
        }

        private static DirectionType GetOppositeDirection(DirectionType direction) => direction switch
        {
            DirectionType.Up => DirectionType.Down,
            DirectionType.Down => DirectionType.Up,
            DirectionType.Left => DirectionType.Right,
            DirectionType.Right => DirectionType.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Unsupported direction: {direction}")
        };
    }
}