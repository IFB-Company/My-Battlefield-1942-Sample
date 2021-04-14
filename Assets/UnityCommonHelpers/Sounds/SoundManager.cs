using Injection;
using UnityEngine;

namespace UnityHelpers.Sounds
{
    public class SoundManager : MonoBehaviour, IInjectable
    {
        [SerializeField] private AudioSource _soundSource;
        public AudioSource SoundSource => _soundSource;
        
        [SerializeField] private AudioSource _musicSource;
        public AudioSource MusicSource => _musicSource;

        public void PlayMusic(AudioClip clip, bool isLoop = true)
        {
            if (_musicSource != null)
            {
                _musicSource.clip = clip;
                _musicSource.loop = true;
                _musicSource.Play();
            }
        }

        public void PlaySound(AudioClip clip)
        {
            if (_soundSource != null)
            {
                _soundSource.PlayOneShot(clip);
            }
        }
    }
}
