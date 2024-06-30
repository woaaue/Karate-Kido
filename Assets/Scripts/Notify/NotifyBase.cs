using UnityEngine;

public class NotifyBase : MonoBehaviour
{
    [SerializeField] private NotifyAnimator _animator;
    [SerializeField] private float _liveTime;

    private bool _isStart;
    private CountdownTimer _timer;

    private void Start() => _animator.Show(StartTimer);

    private void Update()
    {
        if (_isStart)
        {
            var deltaTime = Time.deltaTime;
            _timer.StartTimer(deltaTime);
        }
    }

    private void StartTimer()
    {
        _isStart = true;
        _timer = new CountdownTimer(_liveTime, 0);
        _timer.OnTimerFinished += StopTimer;
    }

    private void StopTimer()
    {
        _timer.OnTimerFinished -= StopTimer;
        _animator.Hide(Destroy);
    }

    private void Destroy() => Destroy(gameObject);
}
