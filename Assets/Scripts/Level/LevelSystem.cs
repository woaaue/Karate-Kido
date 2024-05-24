using System;
using Zenject;

[Serializable]
public class LevelData
{
    public int Level;
    public int CurrentExperience;
    public int ExperienceNextLevel;
}

public sealed class LevelSystem
{
    public event Action OnLevelUp;

    private int _currentLevel = 1;
    private int _currentExperience = 0;

    [Inject]
    public void Construct()
    {

    }

    public LevelData GetData()
    {
        return new LevelData
        {
            Level = _currentLevel,
            CurrentExperience = _currentExperience,
            ExperienceNextLevel = GetExperienceNextLevel(_currentLevel).ExpNextLvl
        };
    }

    public void AddValue(int value)
    {
        var ExpNewlvl = GetExperienceNextLevel(_currentLevel);

        _currentExperience += value;

        if (_currentExperience >= ExpNewlvl.ExpNextLvl)
        {
            EditLevel(ExpNewlvl);
        }
    }

    private Level GetExperienceNextLevel(int level)
    {
        return SettingsProvider.Get<LevelInfo>().GetLevel(_currentLevel);
    }

    private void EditLevel(Level newLevel)
    {
        _currentLevel = newLevel.Number;
        _currentExperience = newLevel.ExpNextLvl - _currentExperience;

        OnLevelUp?.Invoke();
    }
}
