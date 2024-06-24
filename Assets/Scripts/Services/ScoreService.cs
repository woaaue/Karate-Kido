using System;
using Zenject;
using UnityEngine;

public sealed class ScoreService : MonoBehaviour
{
    [SerializeField] private int _countAccrualsScore;

    public event Action OnScoreChanged;
    public event Action OnScoreReseted;

    private Score _score;
    private GameService _gameService;
    private LevelSystemService _levelService;

    private void Start()
    {
        _score = new Score();
        LoadData();


        _gameService.OnPlayerHited += AddValue;
        _gameService.OnPlayerHited += ChangeBestScore;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ChangeBestScore();
            SaveData();
        }
    }

    private void OnDestroy()
    {
        _gameService.OnPlayerHited -= AddValue;
        _gameService.OnPlayerHited -= ChangeBestScore;

        SaveData();
    }

    [Inject]
    public void Construct(GameService gameService, LevelSystemService levelService)
    {
        _gameService = gameService;
        _levelService = levelService;
    }

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
        float currentLevel = _levelService.GetData().CurrentLevel;
        float coefficient = currentLevel + (currentLevel / 10);
        var accrualsScore = Convert.ToInt32(_countAccrualsScore * coefficient);

        _score.AddValue(accrualsScore);
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
