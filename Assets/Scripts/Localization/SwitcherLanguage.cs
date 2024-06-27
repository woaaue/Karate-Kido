using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
public sealed class Language
{
    public int LanguageIndex;
}

public sealed class SwitcherLanguage : MonoBehaviour
{
    private int _currentLocalIndex = 0;

    private void Start() => LoadSelectLanguage();

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

        SaveSelectLanguage();
    }

    private int GetCurrentIndexLocale()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        var locales = LocalizationSettings.AvailableLocales;

        return locales.Locales.IndexOf(currentLocale);
    }

    private Language GetLanguage()
    {
        var currentLanguage = new Language
        {
            LanguageIndex = GetCurrentIndexLocale(),
        };

        return currentLanguage;
    }

    private void SetLanguage(Language language)
    {
        if (language != null)
            _currentLocalIndex = language.LanguageIndex;
        else
            _currentLocalIndex = GetCurrentIndexLocale();

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_currentLocalIndex];
    }

    private void SaveSelectLanguage() => Storage.Save(GetLanguage(), "currentLanguage.json");
    private void LoadSelectLanguage() => SetLanguage(Storage.Load<Language>("currentLanguage.json"));
}