using System;
using UnityEngine;

namespace CatJam
{
    public class Obstacle : IEntity
    {
        public RankType Rank { get; set; }
        public DirectionType Direction { get; set; }
        public bool IsMoveable { get; set; }
        public Vector2Int Position { get; set; }

        private readonly Ground _ground;

        public Obstacle(RankType rank, DirectionType direction, Vector2Int position, bool isMoveable, Ground ground)
        {
            _ground = ground;
            Rank = rank;
            Direction = direction;
            Position = position;
            IsMoveable = isMoveable;
            ground.AddObject(this, position);
        }
    }
}