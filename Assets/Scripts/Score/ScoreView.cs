using TMPro;
using UnityEngine;

public sealed class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ScoreService _scoreService;

    private void Start()
    {
        _scoreService.OnScoreChanged += ChangeScore;
        _scoreService.OnScoreReseted += ChangeScore;
    }

    private void OnDestroy()
    {
        _scoreService.OnScoreChanged -= ChangeScore;
        _scoreService.OnScoreReseted -= ChangeScore;
    }

    private void ChangeScore() => _scoreText.text = _scoreService.GetScore().ToString();
}
