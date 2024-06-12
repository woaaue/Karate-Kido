using System;

[Serializable]
public class ScoreData
{
    public int Score;
    public int BestScore;
}

public class Score
{
    public event Action OnBestScoreChanged;

    private int _currentScore;
    private int _bestScore;

    public ScoreData GetData()
    {
        return new ScoreData
        {
            Score = _currentScore,
            BestScore = _bestScore,
        };
    }

    public void AddValue(int value)
    {
        _currentScore += value;
    }

    public bool ChangeBestScore()
    {
        if (_bestScore < _currentScore) 
        { 
            _bestScore = _currentScore;
            OnBestScoreChanged?.Invoke();
            return true;
        }

        return false;
    }
}
