using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SettingsProvider", menuName = "Karate Tupo/SettingsProvider", order = 1)]
public sealed class SettingsProvider : ScriptableObject
{
    [SerializeField] private List<ScriptableObject> _settingsList;

    private static SettingsProvider _settingsProvider;

    public static T Get<T>() where T : ScriptableObject
    {
        if (_settingsProvider == null)
            _settingsProvider = Resources.Load<SettingsProvider>("SettingsProvider");

        return (T)_settingsProvider._settingsList.First(x => x is T);
    }
}
