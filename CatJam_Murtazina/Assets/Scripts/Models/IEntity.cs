using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatJam
{
    public interface IEntity
    {
        public RankType Rank { get; set; }
        public DirectionType Direction { get; set; }
        public Vector2Int Position { get; set; }
        public bool IsMoveable { get; set; }
    }
}