using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class TimerDriver : MonoBehaviour
    {
        private ITimer _timer;

        [Inject]
        public void Construct(ITimer timer)
        {
            _timer = timer;
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }
    }
}