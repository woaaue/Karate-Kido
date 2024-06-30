using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PrefabSettings", menuName = "Karate Tupo/PrefabSettings", order = 1)]
public sealed class PrefabSettings : ScriptableObject
{
    [SerializeField] List<PopupBase> _popups;
    [SerializeField] List<NotifyBase> _notifications;

    public Popup<T> GetPopup<T>() where T : PopupBaseSettings
    {
        try
        {
            return (Popup<T>)_popups.First(x => x is Popup<T>);
        }
        catch (Exception e) 
        {
            Debug.LogError($"There is no popup with these settings {typeof(T)}");
            throw;
        }
    }

    public Notify<T> GetNotify<T>() where T : NotifyBaseSettings
    {
        try
        {
            return (Notify<T>)_notifications.First(x => x is Notify<T>);
        }
        catch (Exception e)
        {
            Debug.LogError($"There is no notify with these settings {typeof(T)}");
            throw;
        }
    }
}
