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
        path[1] = new Vector3(transform.position.x + (-direction * 1.5f), transform.position.y + 0.5f, transform.position.z);
        path[2] = new Vector3(transform.position.x + (-direction * 3), transform.position.y + 1, transform.position.z);
        path[3] = new Vector3(transform.position.x + (-direction * 4.5f), transform.position.y + 0.5f, transform.position.z);
        path[4] = new Vector3(transform.position.x + (-direction * 6), transform.position.y, transform.position.z);

        Sequence sequence = DOTween.Sequence();

        sequence
            .Append(transform.DOPath(path, 1f, PathType.CatmullRom).SetEase(Ease.Linear))
            .Join(transform.DORotate(new Vector3(0, 0, 1080), 2f, RotateMode.FastBeyond360))
            .Join(_spriteRenderer.DOFade(0, 1f))
            .OnKill(() =>
            {
                callback?.Invoke();
            });
    }
}
