using UnityEngine;

public sealed class NotifyController : MonoBehaviour
{
    [SerializeField] private Transform _notifyParent;

    public void ShowNotify<T>(T settings) where T : NotifyBaseSettings
    {
        var notifyPrefab = SettingsProvider.Get<PrefabSettings>().GetNotify<T>();
        var inctance = Instantiate(notifyPrefab, _notifyParent, false);
        inctance.Setup(settings);
    }
}
