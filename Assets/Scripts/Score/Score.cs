using System;

[Serializable]
public class ScoreData
{
    public int Score;
    public int BestScore;
}

public class Score
{
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

    public void ChangeBestScore()
    {
        if (_bestScore < _score)
            _bestScore = _score;
    }
}
