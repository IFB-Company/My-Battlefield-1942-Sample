using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityHelpers.Sounds
{
    public abstract class SoundDataBase<T> : ScriptableObject
    {
        [System.Serializable]
        protected class SoundContainer
        {
            [SerializeField] protected T _key;
            public T Key => _key;

            [SerializeField] protected AudioClip _audioClip;
            public AudioClip AudioClip => _audioClip;
        }

        [SerializeField] protected SoundContainer[] _soundContainers;

        private Dictionary<T, AudioClip> _clipsDict;
        

        public void InitClipsDict()
        {
            _clipsDict = new Dictionary<T, AudioClip>(_soundContainers.Length);
            foreach (var container in _soundContainers)
            {
                _clipsDict.Add(container.Key, container.AudioClip);
            }
        }

        public AudioClip GetClip(T key)
        {
            if (_clipsDict == null)
            {
                InitClipsDict();
            }
            
            if (_clipsDict.TryGetValue(key, out var clip))
            {
                return clip;
            }

            Debug.LogError($"Clip with key {key.ToString()} is not exists!");
            return null;
        }
    }
}
