using System;

namespace CatJam
{
    public interface ITimer
    {
        event Action<float> OnTick; // оставшееся время (сек)
        event Action OnFinished; // дошли до нуля
        event Action<bool> OnWarning; // перешли в/из «критической зоны»

        float Remaining { get; }
        bool IsRunning { get; }

        void Start(float durationSeconds);
        void Stop();
        void Reset(float durationSeconds);
        void AddTime(float seconds);
        void Update(float deltaTime);  
    }
}