using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.StaticHelpers
{
    public static class CommonHelpingFunctions
    {
        public static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
        {
            return center + new Vector3(
                       (Random.value - 0.5f) * size.x,
                       (Random.value - 0.5f) * size.y,
                       (Random.value - 0.5f) * size.z
                   );
        }

        public static class LocalDataHelpers
        {
            public static void PlayerPrefsSetBool(string key, bool value)
            {
                int valToSave = value ? 1 : 0;
                PlayerPrefs.SetInt(key, valToSave);
            }
            
            public static bool PlayerPrefsGetBool(string key, bool defaultValue = false)
            {
                int intValDefault = defaultValue ? 1 : 0;
                var prefsVal = PlayerPrefs.GetInt(key, intValDefault);
                return Convert.ToBoolean(prefsVal);
            }
        }
    }
}
