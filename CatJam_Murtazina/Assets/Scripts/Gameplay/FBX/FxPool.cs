using UnityEngine;
using Zenject;

namespace CatJam
{
    public class FxPool : MonoMemoryPool<Vector3, Quaternion, FxInstance>
    {
        protected override void OnCreated(FxInstance item) { item.gameObject.SetActive(false); }
        protected override void Reinitialize(Vector3 pos, Quaternion rot, FxInstance item)
        {
           
        }
    }
}