using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class TreeAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float fistPathY;
    [SerializeField] private float fistPathX;
    [SerializeField] private float secondPathY;
    [SerializeField] private float secondPathX;
    [SerializeField] private float thirdPathY;
    [SerializeField] private float thirdPathX;
    [SerializeField] private float fouthPathY;
    [SerializeField] private float fouthPathX;

    public event Action OnAnimationCompleted;

    public void FlyTree(float direction)
    {
        CreateAnimationFly(direction, OnAnimationCompleted);
    }

    private void CreateAnimationFly(float direction, Action callback = null)
    {
        Vector3[] path = new Vector3[5];
        path[0] = transform.position;
        path[1] = new Vector3(transform.position.x + (-direction * fistPathX), transform.position.y + fistPathY, transform.position.z);
        path[2] = new Vector3(transform.position.x + (-direction * secondPathX), transform.position.y - secondPathY, transform.position.z);
        path[3] = new Vector3(transform.position.x + (-direction * thirdPathX), transform.position.y - thirdPathY, transform.position.z);
        path[4] = new Vector3(transform.position.x + (-direction * fouthPathX), transform.position.y - fouthPathY, transform.position.z);

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
