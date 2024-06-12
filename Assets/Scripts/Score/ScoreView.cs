using TMPro;
using Zenject;
using UnityEngine;

public sealed class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private ScoreService _scoreService;

    private void Start()
    {
        _scoreService.OnScoreChanged += ChangeScore;
        ChangeScore();
    }
    private void OnDestroy() => _scoreService.OnScoreChanged -= ChangeScore;

    [Inject]
    public void Construct(ScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    private void ChangeScore() => _scoreText.text = _scoreService.GetData().Score.ToString();
}
