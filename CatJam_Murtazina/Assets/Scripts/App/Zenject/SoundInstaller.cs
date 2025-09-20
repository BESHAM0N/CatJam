using System.Collections.Generic;
using CatJam.Sound;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip catClickTrue;
        [SerializeField] private AudioClip catClickFalse;
        [SerializeField] private AudioClip victory;
        [SerializeField] private AudioClip defeat;

        public override void InstallBindings()
        {
            var clips = new Dictionary<SoundType, AudioClip>
            {
                { SoundType.BackgroundMusic, backgroundMusic },
                { SoundType.ButtonClick, buttonClick },
                { SoundType.CatClickTrue, catClickTrue },
                { SoundType.CatClickFalse, catClickFalse },
                { SoundType.Victory, victory },
                { SoundType.Defeat, defeat },
            };

            Container.Bind<ISoundService>()
                .To<SoundService>()
                .AsSingle()
                .WithArguments(sfxSource, musicSource, clips);
        }
    }
}