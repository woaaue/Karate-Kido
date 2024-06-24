using UnityEngine;
using JetBrains.Annotations;

public sealed class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [UsedImplicitly]
    public void PlayIdle() => _animator.Play("Idle");

    public void PlayAttack() => _animator.Play("PlayerAttack");
}
