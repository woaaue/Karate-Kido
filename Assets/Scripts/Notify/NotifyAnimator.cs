using System;
using DG.Tweening;
using UnityEngine;

public sealed class NotifyAnimator : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Show(Action callback = null)
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(transform.DOLocalMove(new Vector3(0, 400, 0), 1f))
            .Join(_canvasGroup.DOFade(1, 1f))
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }

    public void Hide(Action callback = null)
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(transform.DOLocalMove(new Vector3(0, 500, 0), 1f))
            .Join(_canvasGroup.DOFade(0, 1f))
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }
}
