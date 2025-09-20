using System;

namespace Models.PauseMenuModel
{
    public class PauseMenuModel
    {
        public event Action<bool> OnSoundStateChanged;

        public bool IsSoundEnabled { get; private set; } = true;
        public bool IsPaused { get; private set; } = false;

        public void ToggleSound()
        {
            IsSoundEnabled = !IsSoundEnabled;
            OnSoundStateChanged?.Invoke(IsSoundEnabled);
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }
    }
}