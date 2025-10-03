using UnityEngine;
using Zenject;

namespace CatJam
{
    public class FxInstaller : MonoInstaller
    {
        [Header("Scene Roots")]
        [SerializeField] private Transform _fxRoot;
        
        [Header("FX Prefabs (FBX or containers)")]
        [SerializeField] private FxInstance _clickSuccessPrefab;
        [SerializeField] private FxInstance _clickFailPrefab;
        [SerializeField] private FxInstance _buttonClickPrefab;
        
        public override void InstallBindings()
        {
        }
    }
}