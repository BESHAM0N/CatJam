using UnityEngine;

namespace CatJam
{
    public interface IEntity
    {
        public Vector2Int Position { get; set; }
        public bool IsMoveable { get; set; }
    }
}