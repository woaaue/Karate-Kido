using System;

[Serializable]
public class LevelData
{
    public int CurrentLevel;
    public int CurrentExperience;
    public int ExperienceNextLevel;
}

public sealed class LevelSystem
{
    public event Action OnLevelUp;

    private int _currentLevel = 1;
    private int _currentExperience = 0;

    public LevelData GetData()
    {
        return new LevelData
        {
            CurrentLevel = _currentLevel,
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

    private void EditLevel(Level newLevel)
    {
        _currentLevel = newLevel.Number;
        _currentExperience -= GetExperienceNextLevel(_currentLevel - 1).ExpNextLvl;

        OnLevelUp?.Invoke();
    }
}
