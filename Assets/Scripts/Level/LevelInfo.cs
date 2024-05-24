using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Karate Tupo/LevelInfo", order = 1)]
public sealed class LevelInfo : ScriptableObject
{
    [SerializeField] private List<Level> _levels;

    public Level GetLevel(int number)
    {
        try
        {
            return _levels.First(level => level.Number == number);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
}
