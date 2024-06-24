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
        if (xPosition < 0)
        {
            transform.position = new Vector3(-1.3f, 0);
            transform.rotation = new Quaternion(0, -180, 0, 0);
        }
        else
        {
            transform.position = new Vector3(1.3f, 0);
            transform.rotation = Quaternion.identity;
        }

        OnPlayerMoved?.Invoke(gameObject.transform.position);
    }
}
