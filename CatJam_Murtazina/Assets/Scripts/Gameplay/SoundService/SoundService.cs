using System.Collections.Generic;
using UnityEngine;

namespace CatJam.Sound
{
    public sealed class SoundService : ISoundService
    {
        private readonly AudioSource _sfxSource;
        private readonly AudioSource _musicSource;
        private readonly Dictionary<SoundType, AudioClip> _clips;

        public bool IsSoundEnabled { get; private set; } = true;

        public SoundService(AudioSource sfxSource, AudioSource musicSource, Dictionary<SoundType, AudioClip> clips)
        {
            _sfxSource = sfxSource;
            _musicSource = musicSource;
            _clips = clips;

            _musicSource.loop = true;
        }

        public void PlaySound(SoundType type)
        {
            if (!IsSoundEnabled || !_clips.TryGetValue(type, out var clip)) return;

            if (type == SoundType.BackgroundMusic)
            {
                if (_musicSource.clip != clip)
                {
                    _musicSource.clip = clip;
                    _musicSource.Play();
                }
            }
            else
            {
                _sfxSource.PlayOneShot(clip);
            }
        }

        public void ToggleSound(bool enabled)
        {
            IsSoundEnabled = enabled;
            _sfxSource.mute = !enabled;
            _musicSource.mute = !enabled;
        }
    }
}