using System;
using DG.Tweening;
using UnityEngine;

public sealed class TreeAnimator : MonoBehaviour
{
    public event Action OnAnimationCompleted;

    public void FlyTree(float direction)
    {
        CreateAnimationFly(direction, OnAnimationCompleted);
    }

    private void CreateAnimationFly(float direction, Action callback = null)
    {
        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(gameObject.transform.DOMove(new Vector2(1, 1), 1))
            .Join(gameObject.transform.DOMove(new Vector2(1, 1), 1))
            .Join(gameObject.transform.DOMove(new Vector2(1, 1), 1))
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }
}
