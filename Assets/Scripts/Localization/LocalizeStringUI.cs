using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.Localization.Settings;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class LocalizeStringUI : MonoBehaviour
{
    [SerializeField] private string _key;

    private TextMeshProUGUI _text;
    private SwitcherLanguage _language;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        SetValue();
    }

    private void OnEnable()
    {
        _language.OnLanguageChanged += SetValue;

        if (_text != null)
            SetValue();
    }

    private void OnDisable() => _language.OnLanguageChanged -= SetValue;

    [Inject]
    public void Construct(SwitcherLanguage language) => _language = language;

    private async void SetValue()
    {
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_key);
        _text.text = await localizedString.Task;
    }
}
