using TMPro;
using UnityEngine;

public sealed class NewLevelNotify : Notify<NewLevelNotifySettings>
{
    [SerializeField] private GameObject _levelInfo;
    [SerializeField] private GameObject _modifierInfo;
    [SerializeField] private TextMeshProUGUI _nextLevel;
    [SerializeField] private TextMeshProUGUI _nextModifier;
    [SerializeField] private TextMeshProUGUI _previousLevel;
    [SerializeField] private TextMeshProUGUI _previousModifier;

    public override void Setup(NewLevelNotifySettings settings)
    {
        _nextLevel.text = settings.Info.CurrentLevel.ToString();
        _previousLevel.text = settings.Info.PreviousLevel.ToString();
        _nextModifier.text = settings.Info.CurrentCoefficient.ToString();
        _previousModifier.text = settings.Info.PreviousCoefficient.ToString();

        Invoke("SwitchPanelInfo", 2.5f);
    }

    private void SwitchPanelInfo()
    {
        _levelInfo.SetActive(false);
        _modifierInfo.SetActive(true);
    }
}

public sealed class NewLevelNotifySettings : NotifyBaseSettings
{
    public InfoData Info;
}
