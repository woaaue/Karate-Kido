using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class TreeAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public event Action OnAnimationCompleted;

    public void FlyTree(float direction)
    {
        CreateAnimationFly(direction, OnAnimationCompleted);
    }

    private void CreateAnimationFly(float direction, Action callback = null)
    {
        Vector3[] path = new Vector3[5];
        path[0] = transform.position;
        path[1] = new Vector3(transform.position.x + (-direction * 1), transform.position.y + 0, transform.position.z);
        path[2] = new Vector3(transform.position.x + (-direction * 1.7f), transform.position.y - 0.5f, transform.position.z);
        path[3] = new Vector3(transform.position.x + (-direction * 2f), transform.position.y - 1f, transform.position.z);
        path[4] = new Vector3(transform.position.x + (-direction * 2.2f), transform.position.y - 2.2f, transform.position.z);

        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(transform.DOPath(path, 0.7f, PathType.CatmullRom).SetEase(Ease.Linear))
            .Join(transform.DORotate(new Vector3(0, 0, 1080), 0.9f, RotateMode.FastBeyond360))
            .Join(transform.DOScale(0, 1f))
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }
}
