using System.Collections.Generic;
using UnityEngine;

namespace TD.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private List<AudioProperty> _audioProperties;

        private const string MusicEnabledKey = "MusicEnabled";
        private const string SfxEnabledKey = "SFXEnabled";

        public bool IsMusicEnabled { get; private set; } = true;
        public bool IsSfxEnabled { get; private set; } = true;

        private void Awake()
        {
            Instance = this;

            IsMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1;
            IsSfxEnabled = PlayerPrefs.GetInt(SfxEnabledKey, 1) == 1;
            ApplyMuteStates();
        }

        public void Start()
        {
            if (IsMusicEnabled)
                PlayAudio(AudioType.LevelBG);
        }

        public void PlayAudio(AudioType audioType)
        {
            if (audioType == AudioType.LevelBG && !IsMusicEnabled)
                return;
            if (audioType != AudioType.LevelBG && !IsSfxEnabled)
                return;

            foreach (var audioProperty in _audioProperties)
            {
                if (audioProperty.audioType == audioType)
                    audioProperty.audioSource.Play();
            }
        }

        public void ToggleMusic()
        {
            SetMusicEnabled(!IsMusicEnabled);
        }

        public void ToggleSfx()
        {
            SetSfxEnabled(!IsSfxEnabled);
        }

        public void SetMusicEnabled(bool enabled)
        {
            IsMusicEnabled = enabled;
            PlayerPrefs.SetInt(MusicEnabledKey, enabled ? 1 : 0);
            PlayerPrefs.Save();

            ApplyMuteStates();

            if (enabled)
                PlayAudio(AudioType.LevelBG);
            else
                StopAudio(AudioType.LevelBG);
        }

        public void SetSfxEnabled(bool enabled)
        {
            IsSfxEnabled = enabled;
            PlayerPrefs.SetInt(SfxEnabledKey, enabled ? 1 : 0);
            PlayerPrefs.Save();

            ApplyMuteStates();
        }

        public void StopAudio(AudioType audioType)
        {
            foreach (var audioProperty in _audioProperties)
            {
                if (audioProperty.audioType == audioType)
                    audioProperty.audioSource.Stop();
            }
        }

        private void ApplyMuteStates()
        {
            foreach (var audioProperty in _audioProperties)
            {
                bool isMusic = audioProperty.audioType == AudioType.LevelBG;
                audioProperty.audioSource.mute = isMusic ? !IsMusicEnabled : !IsSfxEnabled;
            }
        }
    }

    [System.Serializable]
    public class AudioProperty
    {
        public AudioSource audioSource;
        public AudioType audioType;
    }
}