using System;
using UnityEditorInternal;

public class InfoData
{
    public int CurrentLevel;
    public int PreviousLevel;
    public float CurrentCoefficient;
    public float PreviousCoefficient;
}

[Serializable]
public class LevelData
{
    public int CurrentLevel;
    public float Coefficient;
    public int CurrentExperience;
    public int ExperienceNextLevel;
}

public sealed class LevelSystem
{
    public event Action OnLevelUp;

    private int _currentLevel = 1;
    private float _coefficient = 1f;
    private int _currentExperience = 0;

    public InfoData GetInfo()
    {
        return new InfoData
        {
            CurrentLevel = _currentLevel,
            PreviousLevel = GetExperienceNextLevel(_currentLevel - 1).Number,
            CurrentCoefficient = _coefficient,
            PreviousCoefficient = GetExperienceNextLevel(_currentLevel - 1).Coefficient,
        };
    }

    public LevelData GetData()
    {
        return new LevelData
        {
            CurrentLevel = _currentLevel,
            Coefficient = GetCoefficient(),
            CurrentExperience = _currentExperience,
            ExperienceNextLevel = GetExperienceNextLevel(_currentLevel).ExpNextLvl
        };
    }

    public void SetLevelData(LevelData data)
    {
        if (data == null) 
        {
            GetData();
        }
        else 
        {
            _coefficient = data.Coefficient;
            _currentLevel = data.CurrentLevel;
            _currentExperience = data.CurrentExperience;
        }
    }

    public void AddValue(int value)
    {
        var ExpNewlvl = GetExperienceNextLevel(_currentLevel);

        _currentExperience += value;

        if (_currentExperience >= ExpNewlvl.ExpNextLvl)
        {
            EditLevel(GetExperienceNextLevel(_currentLevel + 1));
        }
    }

    private Level GetExperienceNextLevel(int level)
    {
        return SettingsProvider.Get<LevelInfo>().GetLevel(level);
    }

    private float GetCoefficient()
    {
        return SettingsProvider.Get<LevelInfo>().GetLevel(_currentLevel).Coefficient;
    }

    private void EditLevel(Level newLevel)
    {
        _currentLevel = newLevel.Number;
        _coefficient = newLevel.Coefficient;
        _currentExperience -= GetExperienceNextLevel(_currentLevel - 1).ExpNextLvl;

        OnLevelUp?.Invoke();
    }
}
