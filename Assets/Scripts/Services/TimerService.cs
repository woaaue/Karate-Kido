using System;
using Zenject;
using UnityEngine;

public sealed class TimerService : MonoBehaviour
{
    [SerializeField] private int _timeInSeconds;
    [SerializeField] private int _timePerImpact;

    public int StartTime => _timeInSeconds;
    public event Action OnTimerStarted;
    public event Action<string> OnUpTimed;
    public event Action<float> OnTimerValueChanged;

    private bool _isActive;
    private CountdownTimer _countdownTimer;
    [Inject] private GameService _gameService;

    private void Start()
    {
        _gameService.OnPlayerHited += AddTime;
    }

    private void Update()
    {
        if (_isActive)
        {
            var deltaTime = Time.deltaTime;
            _countdownTimer.StartTimer(deltaTime);
        }
    }

    public void StartTimer()
    {
        _countdownTimer = new CountdownTimer(_timeInSeconds, _timePerImpact);
        Subscribe();
        _isActive = true;
        OnTimerStarted?.Invoke();
    }

    public void StopTimer()
    {
        _isActive = false;
        UnSubcribe();
    }

    private void Subscribe()
    {
        _countdownTimer.OnTimerFinished += UpTime;
        _countdownTimer.OnValueChanged += GetTimed;
    }

    private void UnSubcribe()
    {
        _countdownTimer.OnTimerFinished -= UpTime;
        _countdownTimer.OnValueChanged -= GetTimed;
    }

    private void AddTime() => _countdownTimer.AddValue();
    private void UpTime() => OnUpTimed?.Invoke("Time's up");
    private void GetTimed(float value) => OnTimerValueChanged?.Invoke(value);
}
