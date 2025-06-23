using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatJam
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Music Clips")] 
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;

        [Header("SFX Clips")] 
        [SerializeField] private AudioClip _clickSuccessClip;
        [SerializeField] private AudioClip _clickFailClip;
        [SerializeField] private AudioClip _gameOverClip;

        private AudioSource _musicSource;
        private AudioSource _sfxSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;

            _sfxSource = gameObject.AddComponent<AudioSource>();
            _sfxSource.playOnAwake = false;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "MenuScene":
                    PlayMusic(_menuMusic);
                    break;
                case "GameScene":
                    PlayMusic(_gameMusic);
                    break;
                default:
                    StopMusic();
                    break;
            }
        }

        public void PlayMusic(AudioClip clip)
        {
            if (_musicSource.clip == clip && _musicSource.isPlaying) return;
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
            _musicSource.clip = null;
        }

        public void PlaySuccess() => _sfxSource.PlayOneShot(_clickSuccessClip);
        public void PlayFail() => _sfxSource.PlayOneShot(_clickFailClip);
        public void PlayGameOver() => _sfxSource.PlayOneShot(_gameOverClip);
    }
}