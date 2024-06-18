using System;
using Zenject;
using UnityEngine;

public sealed class LevelSystemService : MonoBehaviour
{
    [SerializeField] private int _countAccrualsExperience;

    public event Action OnLevelChange;

    private GameService _gameService;
    private LevelSystem _levelSystem;

    private void Start()
    {
        _levelSystem = new LevelSystem();
        LoadData();

        _levelSystem.OnLevelUp += ChangeLevel;
    }

    private void OnDestroy()
    {
        SaveData();

        _gameService.OnPlayerHit -= AddValue;
        _levelSystem.OnLevelUp -= ChangeLevel;
    }

    [Inject]
    public void Construct(GameService gameService)
    {
        _gameService = gameService;

        _gameService.OnPlayerHit += AddValue;
    }

    public LevelData GetData()
    {
        return _levelSystem.GetData();
    }

    private void ChangeLevel() => OnLevelChange?.Invoke();
    private void AddValue() => _levelSystem.AddValue(_countAccrualsExperience);
    private void SaveData() => Storage.Save(_levelSystem.GetData(), "levelData.json");
    private void LoadData() => _levelSystem.SetLevelData(Storage.Load<LevelData>("levelData.json"));
}
