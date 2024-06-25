using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public sealed class SwitcherLanguage : MonoBehaviour
{
    private int _currentLocalIndex = 0;

    private void Start() => _currentLocalIndex = GetCurrentIndexLocale();

    [UsedImplicitly]
    public void SwitchLocale() => InitializeLocalization();

    private void InitializeLocalization()
    {
        AsyncOperationHandle initializeOperation = LocalizationSettings.InitializationOperation;
        initializeOperation.Completed += LocalizationInitialized;
    }

    private void LocalizationInitialized(AsyncOperationHandle handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
            SetLocale();
    }

    private void SetLocale()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        if (locales.Count == 0)
            return;

        _currentLocalIndex = (_currentLocalIndex + 1) % locales.Count;
        LocalizationSettings.SelectedLocale = locales[_currentLocalIndex];

        Debug.Log($"Language switched to: {locales[_currentLocalIndex].Identifier.Code}");
    }

    private int GetCurrentIndexLocale()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        var locales = LocalizationSettings.AvailableLocales;

        return locales.Locales.IndexOf(currentLocale);
    }
}