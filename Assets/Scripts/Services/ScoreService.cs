using System;
using Zenject;
using UnityEngine;

public sealed class ScoreService : MonoBehaviour
{   
    public event Action OnScoreChanged;
    public event Action OnScoreReseted;

    private Score _score;
    private GameService _gameService;

    private void Start()
    {
        ResetScore();

        _gameService.OnPlayerHit += AddValue;
        _gameService.OnPlayerHit += ChangeBestScore;
    }

    private void OnDestroy()
    {
        _gameService.OnPlayerHit -= AddValue;
        _gameService.OnPlayerHit -= ChangeBestScore;
    }

    [Inject]
    public void Construct(GameService gameService) =>_gameService = gameService;

    public void ResetScore()
    {
        _score = new Score();
        OnScoreReseted?.Invoke();
    }

    public ScoreData GetData()
    {
        return _score.GetData();
    }

    private void AddValue()
    {
        _score.AddValue(1);
        OnScoreChanged?.Invoke();
    }

    private void ChangeBestScore() => _score.ChangeBestScore();
}
