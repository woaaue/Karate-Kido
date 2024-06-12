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

    private int _score;
    private int _bestScore;

    public ScoreData GetData()
    {
        return new ScoreData
        {
            Score = _score,
            BestScore = _bestScore,
        };
    }

    public void AddValue(int value)
    {
        _score += value;
    }

    public bool ChangeBestScore()
    {
        if (_bestScore < _score) 
        { 
            _bestScore = _score;
            OnBestScoreChanged?.Invoke();
            return true;
        }

        return false;
    }
}
