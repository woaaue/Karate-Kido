using System;
using DG.Tweening;
using UnityEngine;

public sealed class ScoreAnimator : MonoBehaviour
{
    public void PlayAnimation(Action callback = null)
    {
        transform.DOMoveY(1, 0.5f)
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }
}
