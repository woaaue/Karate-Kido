using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Image _timeBar;
    [SerializeField] private Slider _slider;
    [SerializeField] private TimerService _timerService;

    private SynchronizationContext _context;

    private void Start()
    {
        _context = SynchronizationContext.Current;
        _timerService.OnTimerStarted += FillBar;
    }

    private void FillBar()
    {
        _slider.maxValue = _timerService.Time;
        _slider.value = _slider.maxValue;
        //_timeBar.fillAmount = _timerService.Time;
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        _timerService.OnGetTimed += GetValue;
    }

    private void GetValue(double value)
    {
        Debug.Log(value);
        //_context.Post(c => _timeBar.fillAmount = (float)value / _timerService.Time, null);
        _context.Post(c => _slider.value = (float)value, null);
    }
}
