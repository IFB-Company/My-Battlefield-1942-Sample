using System.Collections.Generic;
using Common.Interfaces.Collections;
using UnityEngine;

namespace Common.StaticHelpers
{
    public static class CollectionHelpers
    {
        public static Dictionary<T1, T2> FillDictByKeyableCollection<T1, T2>(IEnumerable<T2> inputCollection)
            where T2 : IPrimaryKeyable<T1>
        {
            if (inputCollection == null)
            {
                Debug.LogError("inputCollection is null!");
                return null;
            }
            var dict = new Dictionary<T1, T2>();
            foreach (var keyable in inputCollection)
            {
                dict.Add(keyable.GetKey(), keyable);
            }

            return dict;
        }
    }
}
