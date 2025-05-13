using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
{
    [SerializeField] private List<K> m_Keys = new();
    [SerializeField] private List<V> m_Values = new();

    public void OnAfterDeserialize()
    {
        Clear();
        if (m_Keys.Count != m_Values.Count) return;
        for (int i = 0; i < m_Keys.Count; i++) Add(m_Keys[i], m_Values[i]);
    }

    public void OnBeforeSerialize()
    {
        m_Keys.Clear();
        m_Values.Clear();
        foreach (KeyValuePair<K, V> pair in this) { m_Keys.Add(pair.Key); m_Values.Add(pair.Value); }
    }
}