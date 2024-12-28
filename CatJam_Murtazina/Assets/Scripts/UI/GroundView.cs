using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class GroundView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cellPrefab;
        
        private Ground _ground;

        [Inject]
        public void Construct(Ground ground)
        {
            _ground = ground ?? throw new ArgumentNullException(nameof(ground));
        }

        private void Awake()
        {
            foreach (var positions in _ground)
            {
                var viewPosition = new Vector3(positions.x, positions.y);
                Instantiate(_cellPrefab, viewPosition, Quaternion.identity, this.transform);
            }
        }
    }
}