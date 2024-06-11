using System;
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
        //_gameService.OnPlayerDied +=
        //_gameService.OnPlayerHit +=
    }

    private void OnDestroy()
    {
        _clickHandler.OnDirectionChanged -= ChangePosition;
        //_gameService.OnPlayerDied -=
        //_gameService.OnPlayerHit -=
    }

    public void Construct(GameService gameService)
    {
        _gameService = gameService;
    }

    private void ChangePosition(float xPosition)
    {
        gameObject.transform.position = 
            xPosition < 0 ? 
            Vector2.left : Vector2.right;

        OnPlayerMoved?.Invoke(gameObject.transform.position);
    }
}
