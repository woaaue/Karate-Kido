using System;
using Zenject;
using UnityEngine;
using Scripts.Control;
using System.Threading;

public sealed class TimerService : MonoBehaviour
{
    [SerializeField] private int _timeInSeconds;
    [SerializeField] private int _timePerImpact;
    [SerializeField] private ClickHandler _clickHandler;

    public int Time => _timeInSeconds;
    public event Action OnTimerStarted;
    public event Action<string> OnUpTimed;
    public event Action<double> OnGetTimed;

    private CountdownTimer _countdownTimer;
    [Inject] private GameService _gameService;

    private void Start() => _gameService.OnPlayerHit += AddTime;
    private void OnDestroy() => StopTimer();

    public void StartTimer()
    {
        _countdownTimer = new CountdownTimer(_timeInSeconds);

        _countdownTimer.OnGetTimed += GetTimeInSec;
        _countdownTimer.OnTimerStoped += UpTime;
        Debug.Log("Timer started");
        OnTimerStarted?.Invoke();
    }

    public void StopTimer()
    {
        _countdownTimer.Stop();
        _countdownTimer.Dispose();
    }

    private void UpTime() => OnUpTimed?.Invoke("Time's up");
    private void AddTime() => _countdownTimer.AddTime(_timePerImpact);
    private void GetTimeInSec(double time) => OnGetTimed?.Invoke(time);
}
