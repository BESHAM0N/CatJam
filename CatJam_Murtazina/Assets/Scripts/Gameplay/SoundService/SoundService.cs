using System.Collections.Generic;
using UnityEngine;

namespace CatJam
{
    public sealed class SoundService : ISoundService
    {
        private readonly AudioSource _musicSource;
        private readonly AudioSource _sfxSource;
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
            Debug.Log($"PlaySound. SoundType: {type}");
            
            if (type == SoundType.BackgroundMusic)
            {
                if (!_musicSource.isPlaying)
                    _musicSource.Play();
            }
            else
            {
                _sfxSource.PlayOneShot(clip);
            }
            
            Debug.Log($"[Probe] Start. clip={_musicSource.clip?.name}, mute={_musicSource.mute}, vol={_musicSource.volume}");
            Debug.Log($"[Probe] Start. clip={_sfxSource.clip?.name}, mute={_sfxSource.mute}, vol={_sfxSource.volume}");
        }

        public void ToggleSound(bool enabled)
        {
            IsSoundEnabled = enabled;
            _sfxSource.mute = !enabled;
            _musicSource.mute = !enabled;
        }
    }
}