using UnityEngine;
using RaahiFishing.Data;

namespace RaahiFishing.Audio
{
    /// <summary>
    /// Manages audio playback and volume control
    /// Single Responsibility: Audio management
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip castSound;
        [SerializeField] private AudioClip splashSound;
        [SerializeField] private AudioClip biteSound;
        [SerializeField] private AudioClip reelSound;
        [SerializeField] private AudioClip successSound;
        [SerializeField] private AudioClip failSound;
        [SerializeField] private AudioClip buttonClickSound;

        private GameSettings gameSettings;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                gameSettings = new GameSettings();
                ApplyVolume();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetMasterVolume(float volume)
        {
            gameSettings.MasterVolume = volume;
            ApplyVolume();
        }

        public void ApplyVolume()
        {
            float volume = gameSettings.MasterVolume;
            if (musicSource != null) musicSource.volume = volume * 0.7f;
            if (sfxSource != null) sfxSource.volume = volume;
        }

        public void PlayCastSound() => PlaySFX(castSound);
        public void PlaySplashSound() => PlaySFX(splashSound);
        public void PlayBiteSound() => PlaySFX(biteSound);
        public void PlayReelSound() => PlaySFX(reelSound);
        public void PlaySuccessSound() => PlaySFX(successSound);
        public void PlayFailSound() => PlaySFX(failSound);
        public void PlayButtonClick() => PlaySFX(buttonClickSound);

        private void PlaySFX(AudioClip clip)
        {
            if (clip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void PlayMusic(AudioClip music)
        {
            if (musicSource != null && music != null)
            {
                musicSource.clip = music;
                musicSource.loop = true;
                musicSource.Play();
            }
        }

        public void StopMusic()
        {
            if (musicSource != null)
            {
                musicSource.Stop();
            }
        }
    }
}
