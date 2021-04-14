using System;
using Common.ServiceLocator;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnityCommonHelpers.ServiceLocator.UsefulServices
{
    public class SoundService : MonoBehaviour, IGameService
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;

        public AudioClip CurrentMusicClip => _musicSource != null ? _musicSource.clip : null;

        private void Awake()
        {
            Assert.IsNotNull(_soundSource, "_soundSource != null");
            Assert.IsNotNull(_musicSource, "_musicSource != null");
        }


        public void SetSoundVolume(float volume)
        {
            SetSourceVolume(_soundSource, volume);
        }
        
        public void SetMusicVolume(float volume)
        {
            SetSourceVolume(_musicSource, volume);
        }
        
        private void SetSourceVolume(AudioSource source, float volume)
        {
            if (source != null)
            {
                source.volume = volume;
            }
        }

        public void MuteSound()
        {
            SetActiveMuteSource(_soundSource, true);
        }

        public void MuteMusic()
        {
            SetActiveMuteSource(_musicSource, true);
        }
        
        public void UnMuteSound()
        {
            SetActiveMuteSource(_soundSource, false);
        }

        public void UnMuteMusic()
        {
            SetActiveMuteSource(_musicSource, false);
        }

        private void SetActiveMuteSource(AudioSource audioSource, bool isActive)
        {
            if (audioSource != null)
            {
                audioSource.mute = isActive;
            }
        }

        public void PlaySound(AudioClip audioClip)
        {
            if (_soundSource != null)
            {
                _soundSource.PlayOneShot(audioClip);
            }
        }

        public void PlayMusic(AudioClip audioClip, bool isLoop = true)
        {
            if (_musicSource != null)
            {
                _musicSource.loop = isLoop;
                _musicSource.clip = audioClip;
                _musicSource.Play();
            }
        }
    }
}
