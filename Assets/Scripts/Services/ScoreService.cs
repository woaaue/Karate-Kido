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
        _score = new Score();
        LoadData();


        _gameService.OnPlayerHit += AddValue;
        _gameService.OnPlayerHit += ChangeBestScore;
    }

    private void OnDestroy()
    {
        _gameService.OnPlayerHit -= AddValue;
        _gameService.OnPlayerHit -= ChangeBestScore;

        SaveData();
    }

    [Inject]
    public void Construct(GameService gameService) => _gameService = gameService;

    public int GetScore()
    {
        return _score.CurrentScore;
    }

    public int GetBestScore()
    {
        return _score.BestScore;
    }

    public void ResetScore()
    {
        _score.RemoveScore();
        OnScoreReseted?.Invoke();
    }

    private void AddValue()
    {
        _score.AddValue();
        OnScoreChanged?.Invoke();
    }

    private void LoadData()
    {
        var loadedData = Storage.Load<ScoreData>("scoreData.json");
        SetData(loadedData);
    }

    private void SaveData()
    {
        ScoreData data = new ScoreData
        {
            BestScore = _score.BestScore,
        };

        Storage.Save(data, "scoreData.json");
    }

    private void ChangeBestScore() => _score.ChangeBestScore();
    private void SetData(ScoreData data) => _score.SetData(data);
}
