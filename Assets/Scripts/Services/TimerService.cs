using System;
using Zenject;
using UnityEngine;

public sealed class TimerService : MonoBehaviour
{
    [SerializeField] private int _timeInSeconds;
    [SerializeField] private int _timePerImpact;
    [SerializeField] private PopupController _popupController;

    public event Action OnTimerStarted;
    public event Action<double> OnGetTimed;

    private CountdownTimer _countdownTimer;
    [Inject] private GameService _gameService;

    private void Start()
    {
        StartTimer();
        _gameService.OnPlayerHit += AddTime;
    }

    private void OnDestroy()
    {
        _countdownTimer.OnTimerStoped -= UpTime;
        _countdownTimer.OnGetTimed -= GetTimeInSec;
    }

    private void StartTimer()
    {
        _countdownTimer = new CountdownTimer(_timeInSeconds);

        _countdownTimer.OnGetTimed += GetTimeInSec;
        _countdownTimer.OnTimerStoped += UpTime;

        Debug.Log($"Time {_timeInSeconds}");
        OnTimerStarted?.Invoke();
    }

    private void GetTimeInSec(double time)
    {
        Debug.Log($"Remaining time {time}");
        OnGetTimed?.Invoke(time);
    }

    private void AddTime() => _countdownTimer.AddTime(_timePerImpact);

    private void UpTime()
    {
        Debug.Log("Time's up");
        ShowEndGamePopup("Time's up");
    }

    private void ShowEndGamePopup(string message)
    {
        _popupController.ShowPopup(new EndGamePopupSettings
        {
            InfoText = message,
        });
    }
}
