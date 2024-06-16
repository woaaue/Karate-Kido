using System;

public sealed class CountdownTimer
{
    public event Action OnTimerFinished;
    public event Action<float> OnValueChanged;

    private float _addValue;
    private float _startTime;
    private float _currentTime;

    public CountdownTimer (float startTime, float addTime)
    {
        _startTime = startTime;
        _addValue = addTime;
        _currentTime = _startTime;
    }

    public void StartTimer(float deltaTime)
    {
        _currentTime -= deltaTime;
        OnValueChanged?.Invoke(_currentTime); 

        if (_currentTime <= 0) 
        {
            _currentTime = 0;
            OnTimerFinished?.Invoke();
        }
    }

    public void AddValue()
    {
        if (_currentTime + _addValue > _startTime)
            _currentTime = _startTime;
        else
            _currentTime += _addValue;
    }
}
