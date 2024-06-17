using System;

[Serializable]
public class ScoreData
{
    public int BestScore;
}

public class Score
{
    public int CurrentScore => _score;
    public int BestScore => _bestScore;

    private int _score;
    private int _bestScore;

    public void ChangeBestScore()
    {
        if (_bestScore < _score)
            _bestScore = _score;
    }

    public void RemoveScore() => _score = 0;
    public void AddValue(int value = 1) => _score += value;
    public void SetData(ScoreData data) => _bestScore = data.BestScore;
}
