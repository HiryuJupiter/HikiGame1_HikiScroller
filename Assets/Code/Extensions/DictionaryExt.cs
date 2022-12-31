using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExt {

    public delegate void DictionaryOperation<T, K>(T key);
    /// <summary>
    /// Perform an operation for each element in a dictionary.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    /// <param name="dict"></param>
    /// <param name="operation"></param>
    public static void ForEach<T,K>(this Dictionary<T,K> dict, DictionaryOperation<T, K> operation) {
        List<T> keys = new List<T>();
        keys.AddRange(dict.Keys);
        foreach(T key in keys) {
            operation(key);
        }
    }


}
