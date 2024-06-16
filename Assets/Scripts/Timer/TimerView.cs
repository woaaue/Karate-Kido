using UnityEngine;
using UnityEngine.UI;

public sealed class TimerView : MonoBehaviour
{
    [SerializeField] private Image _timeBar;
    [SerializeField] private TimerService _timerService;

    private void Start() => _timerService.OnTimerStarted += FillBar;
    private void SubscribeEvent() => _timerService.OnTimerValueChanged += GetValue;
    private void GetValue(float value) => _timeBar.fillAmount = value / _timerService.StartTime;

    private void FillBar()
    {
        _timeBar.fillAmount = _timerService.StartTime;
        SubscribeEvent();
    }
}
