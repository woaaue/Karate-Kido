using System;
using System.Timers;

public sealed class CountdownTimer
{
    public event Action OnTimerStoped;
    public event Action<double> OnGetTimed;

    private Timer _timer;
    private int _duration;
    private int _�onstDuration;
    private DateTime _startTime;

    public CountdownTimer(int durationSeconds)
    {
        _�onstDuration = durationSeconds;
        _duration = _�onstDuration;
        _timer = new Timer(200);

        _timer.Elapsed += TimedEvent;
        _timer.AutoReset = true;

        Start();
    }

    public void AddTime(int value)
    {
        if (GetTime() + value < _�onstDuration)
            _duration += value;
    }

    private void Start()
    {
        _startTime = DateTime.Now;
        _timer.Start();
    }

    private void Stop() => _timer.Stop();

    private void Dispose()
    {
        _timer.Elapsed -= TimedEvent;
        _timer.Dispose();
    }

    private double GetTime()
    {
        var timeSeconds = (DateTime.Now - _startTime).TotalSeconds;

        return Math.Max(0, _duration - timeSeconds);
    }

    private void TimedEvent(Object source, ElapsedEventArgs e)
    {
        var remainingTime = GetTime();

        OnGetTimed?.Invoke(remainingTime);

        if (remainingTime <= 0) 
        {
            Stop();
            OnTimerStoped?.Invoke();
            Dispose();
        }
    }
}
