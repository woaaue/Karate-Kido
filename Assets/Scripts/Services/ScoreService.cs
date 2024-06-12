using System;
using Zenject;
using UnityEngine;

public sealed class ScoreService : MonoBehaviour
{   
    public event Action OnScoreChanged;
    public event Action OnBestScoreChanged;

    private Score _score;
    private GameService _gameService;

    private void Start()
    {
        _score = new Score();

        _gameService.OnPlayerHit += AddValue;
        _gameService.OnPlayerDied += ChangeBestScore;
    }

    private void OnDestroy()
    {
        _gameService.OnPlayerHit -= AddValue;
        _gameService.OnPlayerDied -= ChangeBestScore;
    }

    [Inject]
    public void Construct(GameService gameService) => _gameService = gameService;

    public ScoreData GetData()
    {
        return _score.GetData();
    }

    private void AddValue()
    {
        _score.AddValue(1);
        OnScoreChanged?.Invoke();
    }

    private void ChangeBestScore()
    {
        if (_score.ChangeBestScore())
            OnBestScoreChanged?.Invoke();
    }
}
