using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class SoundInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip catClickTrue;
        [SerializeField] private AudioClip catClickFalse;
        [SerializeField] private AudioClip defeat;

        public override void InstallBindings()
        {
            var clips = new Dictionary<SoundType, AudioClip>
            {
                { SoundType.BackgroundMusic, backgroundMusic },
                { SoundType.ButtonClick, buttonClick },
                { SoundType.CatClickTrue, catClickTrue },
                { SoundType.CatClickFalse, catClickFalse },
                { SoundType.Defeat, defeat },
            };

            Container.Bind<ISoundService>().To<SoundService>().AsSingle().WithArguments(sfxSource, musicSource, clips);
            Container.BindInterfacesAndSelfTo<StartLevelSoundObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CatClickAndPlaySoundObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PauseMenuAndSoundClickObserver>().AsSingle().NonLazy();
        }
    }
}