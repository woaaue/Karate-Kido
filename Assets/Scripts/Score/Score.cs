using System;
using Zenject;

[Serializable]
public class ScoreData
{
    public int Score;
    public int BestScore;
}

public class Score
{
    private int _currentScore;
    private int _bestScore;

    [Inject]
    public void Construct()
    {

    }

    public ScoreData GetData()
    {
        return new ScoreData
        {
            Score = _currentScore,
            BestScore = _bestScore,
        };
    }

    private void AddValue(int value)
    {
        _currentScore += value;
    }

    private void ChangeBestScore()
    {
        if (_bestScore < _currentScore) 
        { 
            _bestScore = _currentScore;
        }
    }
}
