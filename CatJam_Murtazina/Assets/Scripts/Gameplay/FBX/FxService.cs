using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CatJam 
{
    public class FxService : IFxService
    {
        private readonly Dictionary<FxType, FxPool> _pools;
        private readonly Transform _root;

        public FxService(Dictionary<FxType, FxPool> pools, [Inject(Id = "FxRoot")] Transform fxRoot)
        {
            _pools = pools;
            _root = fxRoot;
        }

        public void PlayFx(FxType type, Vector3 worldPos)
        {
            if (!_pools.TryGetValue(type, out var pool) || pool == null)
            {
                Debug.LogWarning($"[Fx] Pool not found for {type}");
                return;
            }

            var instance = pool.Spawn(worldPos, Quaternion.identity);
            instance.transform.SetParent(_root, true);
        }

        public void PlayFx(FxType type, Transform anchor)
        {
            if (!anchor) return;
            PlayFx(type, anchor.position);
        }
    }
}