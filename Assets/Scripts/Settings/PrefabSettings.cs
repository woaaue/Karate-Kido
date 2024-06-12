using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PrefabSettings", menuName = "Karate Tupo/PrefabSettings", order = 1)]
public sealed class PrefabSettings : ScriptableObject
{
    [SerializeField] List<PopupBase> _popups;

    public Popup<T> GetPopup<T>() where T : PopupBaseSettings
    {
        try
        {
            return (Popup<T>)_popups.First(x => x is T);
        }
        catch (Exception e) 
        {
            Debug.LogError($"There is no popup with these settings {typeof(T)}");
            throw;
        }
    }
}
