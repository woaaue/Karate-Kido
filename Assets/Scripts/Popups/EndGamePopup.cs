using TMPro;
using Zenject;
using UnityEngine;

public sealed class EndGamePopup : Popup<EndGamePopupSettings>
{
    [SerializeField] private TextMeshProUGUI _textInfo;
    [SerializeField] private TextMeshProUGUI _bestResult;

    private ScoreService _scoreService;

    [Inject]
    public void Construct(ScoreService scoreService)
    {
        _scoreService = scoreService;
        GetResult();
    }

    public override void Setup(EndGamePopupSettings settings)
    {
        _textInfo.text = settings.InfoText;
    }

    private void GetResult()
    {
        _bestResult.text = _scoreService.GetBestScore().ToString();
    }
}

public sealed class EndGamePopupSettings : PopupBaseSettings
{
    public string InfoText;
}
