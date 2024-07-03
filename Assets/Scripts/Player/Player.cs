using System;
using Zenject;
using UnityEngine;
using Scripts.Control;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private ClickHandler _clickHandler;

    public event Action<Vector2> OnPlayerMoved;

    private GameService _gameService;

    private void Start()
    {
        _clickHandler.OnDirectionChanged += ChangePosition;
        _gameService.OnPlayerHited += Hit;
        //_gameService.OnPlayerDied +=
    }

    private void OnDestroy()
    {
        _clickHandler.OnDirectionChanged -= ChangePosition;
        _gameService.OnPlayerHited -= Hit;
        //_gameService.OnPlayerDied -=
    }

    [Inject]
    public void Construct(GameService gameService) => _gameService = gameService;

    private void Hit() => _animator.PlayAttack();

    private void ChangePosition(float xPosition)
    {
        if (xPosition < 0)
        {
            transform.position = new Vector3(-1.3f, transform.position.y);
            transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        else
        {
            transform.position = new Vector3(1.3f, transform.position.y);
            transform.rotation = Quaternion.identity;
        }

        OnPlayerMoved?.Invoke(gameObject.transform.position);
    }
}
