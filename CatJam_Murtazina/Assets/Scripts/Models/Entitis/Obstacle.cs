using System;
using UnityEngine;

namespace CatJam
{
    public class Obstacle : IEntity
    {
        public bool IsMoveable { get; set; }
        public Vector2Int Position { get; set; }

        public Obstacle(Vector2Int position, bool isMoveable, Ground ground)
        {
            Position = position;
            IsMoveable = isMoveable;
            ground.AddObject(this, position);
        }
    }
}