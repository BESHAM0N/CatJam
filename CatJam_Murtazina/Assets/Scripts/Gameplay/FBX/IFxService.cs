using UnityEngine;

namespace CatJam
{
    public interface IFxService
    {
        void PlayFx(FxType type, Vector3 worldPos);
        void PlayFx(FxType type, Transform anchor);
    }
}