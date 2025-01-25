using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatJam
{
    public sealed class EntityFactory
    {
        public List<Cat> CreatedCats => _createdCats; 
        public List<Obstacle> AllObstacles => _allObstacles; 
        
        private readonly Ground _ground;
        private readonly HashSet<Vector2Int> _usedPositions = new();
        private readonly List<Cat> _createdCats = new();
        private readonly List<Obstacle> _allObstacles = new();
        private readonly Dictionary<RankType, List<Vector2Int>> _exitPointsByRank = new();
        private static int _rankCount => Enum.GetValues(typeof(RankType)).Length;

        public EntityFactory(Ground ground)
        {
            _ground = ground ?? throw new ArgumentNullException(nameof(ground));
        }

        public void GenerateCats(int totalCatCount)
        {
            foreach (RankType rank in Enum.GetValues(typeof(RankType)))
            {
                _exitPointsByRank[rank] = new List<Vector2Int>();
                var rankCatCount = CalculateCatsForRank(rank, totalCatCount);

                for (var i = 0; i < rankCatCount; i++)
                {
                    var catPosition = GenerateUniquePosition();
                    var direction = DetermineDirectionForCat(catPosition, rank);
                    _exitPointsByRank[rank].Add(GetEdgePoint(catPosition, direction));

                    var cat = new Cat(rank, direction, catPosition, true, _ground);
                    _createdCats.Add(cat);
                }
            }
        }

        public void GenerateObstacles()
        {
            for (var x = 0; x < _ground.Width; x++)
            {
                for (var y = 0; y < _ground.Height; y++)
                {
                    var position = new Vector2Int(x, y);
                    if (_usedPositions.Contains(position)) continue;

                    AddObstacle(position);
                }
            }
        }

        public void CreateBorderObstacles()
        {
            for (var x = 0; x < _ground.Width; x++)
            {
                AddObstacle(new Vector2Int(x, 0));
                AddObstacle(new Vector2Int(x, _ground.Height - 1));
            }

            for (var y = 0; y < _ground.Height; y++)
            {
                AddObstacle(new Vector2Int(0, y));
                AddObstacle(new Vector2Int(_ground.Width - 1, y));
            }
        }

        public void ClearPathsForCats()
        {
            foreach (var cat in _createdCats)
            {
                var pathToExit = GetPathToExit(cat.Position, cat.Direction);
                ClearPathObstacles(pathToExit);
            }
        }

        private int CalculateCatsForRank(RankType rank, int totalCatCount)
        {
            return rank == RankType.FirstLeave ? totalCatCount / _rankCount + totalCatCount % _rankCount : totalCatCount / _rankCount;
        }

        private Vector2Int GenerateUniquePosition()
        {
            Vector2Int position;
            do
            {
                position = new Vector2Int(UnityEngine.Random.Range(0, _ground.Width),
                    UnityEngine.Random.Range(0, _ground.Height));
            } while (_usedPositions.Contains(position));

            _usedPositions.Add(position);
            return position;
        }

        private DirectionType DetermineDirectionForCat(Vector2Int position, RankType rank)
        {
            return rank == RankType.FirstLeave
                ? GetDirectionToEdge(position)
                : GetDirectionToPoint(position, _exitPointsByRank[rank - 1]);
        }

        private void AddObstacle(Vector2Int position)
        {
            var obstacle = new Obstacle(position, false, _ground);
            _allObstacles.Add(obstacle);
            _usedPositions.Add(position);
        }

        private void ClearPathObstacles(IEnumerable<Vector2Int> path)
        {
            foreach (var position in path)
            {
                var obstacle = _allObstacles.FirstOrDefault(o => o.Position == position);
                if (obstacle == null) continue;

                _allObstacles.Remove(obstacle);
                _usedPositions.Remove(position);
                _ground.RemoveObject(obstacle);
            }
        }

        private DirectionType GetDirectionToEdge(Vector2Int position)
        {
            var left = position.x;
            var right = _ground.Width - position.x - 1;
            var up = _ground.Height - position.y - 1;
            var down = position.y;

            var minDistance = Mathf.Min(left, right, up, down);

            return minDistance switch
            {
                _ when minDistance == left => DirectionType.Left,
                _ when minDistance == right => DirectionType.Right,
                _ when minDistance == up => DirectionType.Up,
                _ => DirectionType.Down
            };
        }

        private Vector2Int GetEdgePoint(Vector2Int position, DirectionType direction)
        {
            return direction switch
            {
                DirectionType.Left => new Vector2Int(0, position.y),
                DirectionType.Right => new Vector2Int(_ground.Width - 1, position.y),
                DirectionType.Up => new Vector2Int(position.x, _ground.Height - 1),
                DirectionType.Down => new Vector2Int(position.x, 0),
                _ => position
            };
        }

        private DirectionType GetDirectionToPoint(Vector2Int position, List<Vector2Int> exitPoints)
        {
            var target = exitPoints.OrderBy(point => Vector2Int.Distance(position, point)).FirstOrDefault();
            var direction = target - position;

            return direction.x switch
            {
                < 0 => DirectionType.Left,
                > 0 => DirectionType.Right,
                _ => direction.y > 0 ? DirectionType.Up : DirectionType.Down
            };
        }

        private List<Vector2Int> GetPathToExit(Vector2Int startPosition, DirectionType direction)
        {
            var path = new List<Vector2Int>();
            var offset = direction switch
            {
                DirectionType.Up => new Vector2Int(0, 1),
                DirectionType.Down => new Vector2Int(0, -1),
                DirectionType.Left => new Vector2Int(-1, 0),
                DirectionType.Right => new Vector2Int(1, 0),
                _ => Vector2Int.zero
            };

            var currentPosition = startPosition + offset;

            while (currentPosition.x >= 0 && currentPosition.x < _ground.Width &&
                   currentPosition.y >= 0 && currentPosition.y < _ground.Height)
            {
                path.Add(currentPosition);
                currentPosition += offset;
            }

            return path;
        }

        public void ClearAllObjects()
        {
            _createdCats.Clear();
            _allObstacles.Clear();
            _usedPositions.Clear();
            _exitPointsByRank.Clear();
        }
    }
}
