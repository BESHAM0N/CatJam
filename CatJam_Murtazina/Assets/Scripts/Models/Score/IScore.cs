using System;

namespace CatJam
{
    public interface IScore
    {
        event Action<int> OnStateChanged;
        
        int Current { get; }
        void AddScore();
    }
}