using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public sealed class EndGamePopup : Popup<EndGamePopupSettings>
{
    [SerializeField] private TextMeshProUGUI _textInfo;
    [SerializeField] private TextMeshProUGUI _bestResult;
    [SerializeField] private LocalizeStringEvent _stringEvent;

    private ScoreService _scoreService;

    [Inject]
    public void Construct(ScoreService scoreService)
    {
        _scoreService = scoreService;
        GetResult();
    }

    public override void Setup(EndGamePopupSettings settings)
    {
        LocalizedString local = new LocalizedString
        {
            TableReference = "StringTextLocalization",
            TableEntryReference = settings.LocalizationKey
        };

        _stringEvent.StringReference = local;
        local.StringChanged += UpdateText;
    }

    private void UpdateText(string localizedText) => _textInfo.text = localizedText;
    private void GetResult() => _bestResult.text = _scoreService.GetBestScore().ToString();
}

public sealed class EndGamePopupSettings : PopupBaseSettings
{
    public string LocalizationKey;
}
