using TMPro;
using UnityEngine;

public sealed class EndGamePopup : Popup<EndGamePopupSettings>
{
    [SerializeField] private TextMeshProUGUI _textInfo;

    public override void Setup(EndGamePopupSettings settings)
    {
        _textInfo.text = settings.Action;
    }
}

public sealed class EndGamePopupSettings : PopupBaseSettings
{
    public string Action;
}
