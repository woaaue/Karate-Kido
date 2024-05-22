using System;
using DG.Tweening;
using UnityEngine;

public sealed class PopupAnimator : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Hide(Action callback = null)
    {
        _canvasGroup.alpha = 0;
        _canvasGroup
            .DOFade(1, _duration)
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }

    public void Show(Action callback = null)
    {
        _canvasGroup
            .DOFade(0, _duration)
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }
}
