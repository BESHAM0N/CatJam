namespace CatJam
{
    public interface ISoundService
    {
        void PlaySound(SoundType type);
        void ToggleSound(bool enabled);
        bool IsSoundEnabled { get; }
    }
}