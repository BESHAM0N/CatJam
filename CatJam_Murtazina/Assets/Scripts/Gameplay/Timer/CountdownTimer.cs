using System;

namespace CatJam
{
    public class CountdownTimer : ITimer
    {
        public event Action<float> OnTick;
        public event Action OnFinished;
        public event Action<bool> OnWarning;
        public float Remaining { get; private set; }
        public bool IsRunning { get; private set; }

        private readonly float _warningThreshold = 8;
        private bool _wasBelowThreshold;
        private bool _isWarning;
        private float _duration;
        
        public void Start(float durationSeconds)
        {
            _duration = Math.Max(0f, durationSeconds);
            Remaining = _duration;
            IsRunning = true;
            _isWarning = false;
            _wasBelowThreshold = Remaining <= _warningThreshold;
           
            if (_isWarning)
            {
                _isWarning = false;
                OnWarning?.Invoke(false);
            }

            OnTick?.Invoke(Remaining);
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset(float durationSeconds)
        {
            Start(durationSeconds);
        }

        public void AddTime(float seconds)
        {
            Remaining = Clamp(Remaining + seconds, 0f, _duration <= 0f ? float.MaxValue : _duration);
            OnTick?.Invoke(Remaining);
            if (Remaining <= 0f)
            {
                IsRunning = false;
                OnFinished?.Invoke();
            }
            UpdateWarningFlag();
        }

        public void Update(float deltaTime)
        {
            if (!IsRunning) return;
            Remaining -= deltaTime;
            if (Remaining <= 0f)
            {
                Remaining = 0f;
                IsRunning = false;
                OnTick?.Invoke(Remaining);
                OnFinished?.Invoke();
                UpdateWarningFlag();
                return;
            }

            OnTick?.Invoke(Remaining);
            UpdateWarningFlag();
        }
        
        private void UpdateWarningFlag()
        {
            bool isNowBelow = Remaining <= _warningThreshold;
            if (isNowBelow && !_wasBelowThreshold)
            {
                _isWarning = true;
                OnWarning?.Invoke(true);
            }
            if (!isNowBelow && _wasBelowThreshold)
            {
                _isWarning = false;
                OnWarning?.Invoke(false);
            }
            _wasBelowThreshold = isNowBelow;
        }
        
        private static float Clamp(float v, float min, float max)
        {
            return v < min ? min : (v > max ? max : v);
        }
    }
}