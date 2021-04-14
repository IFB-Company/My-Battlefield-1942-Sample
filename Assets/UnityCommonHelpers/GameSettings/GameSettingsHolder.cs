using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityHelpers.GameSettings
{
    
    [System.Serializable]
    public class IntValue
    {
        public string KeyName;
        public int Value;
    }

    [System.Serializable]
    public class FloatValue
    {
        public string KeyName;
        public float Value;
    }
    
    [System.Serializable]
    public class StringValue
    {
        public string KeyName;
        public string Value;
    }

    public enum GameSettingsType
    {
        NONE = 0,
        INTEGER = 1,
        FLOAT = 2,
        STRING = 3
    }
    
    [System.Serializable]
    public class GameSettingsContainer
    {
        public string KeyName;
        public GameSettingsType SettingsType;
        public string Value;
    }

    [CreateAssetMenu(fileName = "GameSettingsHolder", menuName = "GameSettings/GameSettingsHolder")]
    public class GameSettingsHolder : ScriptableObject
    {
        
        [SerializeField] private GameSettingsContainer[] _settingsContainers;

        [Space]
        [SerializeField] private IntValue[] _intValues;

        [SerializeField] private FloatValue[] _floatValues;

        [SerializeField] private StringValue[] _stringValues;
        
        private Dictionary<GameSettingsType, Dictionary<string, GameSettingsContainer>> SettingsTypesDict;

        private Dictionary<string, int> _intValuesDict;
        private Dictionary<string, float> _floatValuesDict;
        private Dictionary<string, string> _stringValuesDict;
        
        public void InitialSetup()
        {
            if (SettingsTypesDict == null)
            {
                InitDict();
            }

            if (_intValues.Length > 0)
            {
                _intValuesDict = new Dictionary<string, int>(_intValues.Length);
                foreach (var value in _intValues)
                {
                    _intValuesDict.Add(value.KeyName, value.Value);
                }
            }
            
            if (_floatValues.Length > 0)
            {
                _floatValuesDict = new Dictionary<string, float>(_floatValues.Length);
                foreach (var value in _floatValues)
                {
                    _floatValuesDict.Add(value.KeyName, value.Value);
                }
            }
            
            if (_stringValues.Length > 0)
            {
                _stringValuesDict = new Dictionary<string, string>(_stringValues.Length);
                foreach (var value in _stringValues)
                {
                    _stringValuesDict.Add(value.KeyName, value.Value);
                }
            }
        }
        
        public int GetInt(string key, int defaultValue = 0)
        {
            var valuesDict = _intValuesDict;
            if (valuesDict != null)
            {
                if (valuesDict.TryGetValue(key, out var val))
                {
                    return val;
                }
            }
            
            if (SettingsTypesDict == null)
            {
                InitDict();
            }

            var settingType = GameSettingsType.INTEGER;

            var containerDict = SettingsTypesDict[settingType];
            if (!containerDict.ContainsKey(key))
            {
                Debug.LogError($"Setting with type '{settingType}' and key '{key}' is not exists!");
                return defaultValue;
            }

            if (int.TryParse(containerDict[key].Value, out var value))
            {
                return value;
            }

            return defaultValue;
        }
        

        public float GetFloat(string key, float defaultValue = 0f)
        {
            var valuesDict = _floatValuesDict;
            if (valuesDict != null)
            {
                if (valuesDict.TryGetValue(key, out var val))
                {
                    return val;
                }
            }
            
            if (SettingsTypesDict == null)
            {
                InitDict();
            }

            var settingType = GameSettingsType.FLOAT;

            var containerDict = SettingsTypesDict[settingType];
            if (!containerDict.ContainsKey(key))
            {
                Debug.LogError($"Setting with type '{settingType}' and key '{key}' is not exists!");
                return defaultValue;
            }

            if (float.TryParse(containerDict[key].Value, out var value))
            {
                return value;
            }

            return defaultValue;
        }
        
        public string GetString(string key, string defaultValue = "")
        {
            var valuesDict = _stringValuesDict;
            if (valuesDict != null)
            {
                if (valuesDict.TryGetValue(key, out var val))
                {
                    return val;
                }
            }
            
            if (SettingsTypesDict == null)
            {
                InitDict();
            }

            var settingType = GameSettingsType.STRING;

            var containerDict = SettingsTypesDict[settingType];
            if (!containerDict.ContainsKey(key))
            {
                Debug.LogError($"Setting with type '{settingType}' and key '{key}' is not exists!");
                return defaultValue;
            }
            

            return containerDict[key].Value;
        }

        private void InitDict()
        {
            SettingsTypesDict = new Dictionary<GameSettingsType, Dictionary<string, GameSettingsContainer>>()
            {
                {GameSettingsType.INTEGER, new Dictionary<string, GameSettingsContainer>()},
                {GameSettingsType.FLOAT, new Dictionary<string, GameSettingsContainer>()},
                {GameSettingsType.STRING, new Dictionary<string, GameSettingsContainer>()}
            };

            var intSettings = _settingsContainers.Where(c => c.SettingsType == GameSettingsType.INTEGER);
            var floatSettings = _settingsContainers.Where(c => c.SettingsType == GameSettingsType.FLOAT);
            var stringSettings = _settingsContainers.Where(c => c.SettingsType == GameSettingsType.STRING);

            SettingsTypesDict[GameSettingsType.INTEGER] = intSettings.ToDictionary(s => s.KeyName);
            SettingsTypesDict[GameSettingsType.FLOAT] = floatSettings.ToDictionary(s => s.KeyName);
            SettingsTypesDict[GameSettingsType.STRING] = stringSettings.ToDictionary(s => s.KeyName);
        }
    }
}
