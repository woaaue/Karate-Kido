using System;
using Zenject;
using UnityEngine;

public sealed class LevelSystemService : MonoBehaviour
{
    [SerializeField] private int _countAccrualsExperience;

    public event Action OnLevelChanged;
    public event Action OnCurrentExperienceChanged;

    private GameService _gameService;
    private LevelSystem _levelSystem;

    private void Awake()
    {
        _levelSystem = new LevelSystem();
        LoadData();

        _levelSystem.OnLevelUp += ChangeLevel;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }

    private void OnDestroy()
    {
        _gameService.OnPlayerHited -= AddValue;
        _levelSystem.OnLevelUp -= ChangeLevel;

        SaveData();
    }

    [Inject]
    public void Construct(GameService gameService)
    {
        _gameService = gameService;
        _gameService.OnPlayerHited += AddValue;
    }

    public LevelData GetData()
    {
        return _levelSystem.GetData();
    }

    public InfoData GetInfo() 
    {
        return _levelSystem.GetInfo();
    }

    private void AddValue()
    {
        _levelSystem.AddValue(_countAccrualsExperience);
        OnCurrentExperienceChanged?.Invoke();
    }

    private void ChangeLevel() => OnLevelChanged?.Invoke();
    private void SaveData() => Storage.Save(_levelSystem.GetData(), "levelData.json");
    private void LoadData() => _levelSystem.SetLevelData(Storage.Load<LevelData>("levelData.json"));
}
