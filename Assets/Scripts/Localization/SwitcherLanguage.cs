using System;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Localization.Settings;

[Serializable]
public sealed class Language
{
    public int LanguageIndex;
}

public sealed class SwitcherLanguage : MonoBehaviour
{
    public event Action OnLanguageChanged;

    private int _currentLocalIndex = 0;

    private void Start() => LoadSelectLanguage();

    [UsedImplicitly]
    public void SwitchLocale() => InitializeLocalizationAsync();

    private async void InitializeLocalizationAsync()
    {
        await LocalizationSettings.InitializationOperation.Task;
        SetLocale();
    }

    private void SetLocale()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        if (locales.Count == 0)
            return;

        _currentLocalIndex = (_currentLocalIndex + 1) % locales.Count;
        LocalizationSettings.SelectedLocale = locales[_currentLocalIndex];

        OnLanguageChanged?.Invoke();
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

        OnLanguageChanged?.Invoke();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_currentLocalIndex];
    }

    private void SaveSelectLanguage() => Storage.Save(GetLanguage(), "currentLanguage.json");
    private void LoadSelectLanguage() => SetLanguage(Storage.Load<Language>("currentLanguage.json"));
}