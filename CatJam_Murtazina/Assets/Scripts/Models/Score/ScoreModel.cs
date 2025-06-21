using System;

namespace CatJam
{
    public sealed class Score : IScore
    {
        public event Action<int> OnStateChanged;

        public int Current => _current;

        private int _current;

        public void AddScore()
        {
            _current++;
            OnStateChanged?.Invoke(_current);
        }
    }
}