using System;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;

public sealed class CountdownTimer
{
    public event Action OnTimerStoped;
    public event Action<double> OnGetTimed;

    private Timer _timer;
    private int _duration;
    private int _�onstDuration;
    private DateTime _startTime;
    private SynchronizationContext _context;


    public CountdownTimer(int durationSeconds)
    {
        _�onstDuration = durationSeconds;
        _duration = _�onstDuration;
        _timer = new Timer(200);

        _timer.Elapsed += TimedEvent;
        _timer.AutoReset = true;

        _context = SynchronizationContext.Current;

        Start();
    }

    public void AddTime(int value)
    {
        if (GetTime() + value < _�onstDuration)
            _duration += value;
    }

    public void Stop() => _timer.Stop();

    public void Dispose()
    {
        _timer.Elapsed -= TimedEvent;
        _timer.Dispose();
    }

    private void Start()
    {
        _startTime = DateTime.Now;
        _timer.Start();
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
            _context.Post(actionEvent => OnTimerStoped?.Invoke(), null);
            Dispose();
        }
    }
}
