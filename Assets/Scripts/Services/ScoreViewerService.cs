using Zenject;
using UnityEngine;

public sealed class ScoreViewerService : MonoBehaviour
{
    [SerializeField] private ScorePool _pool;

    private ScoreService _scoreService;

    private void Start() => _scoreService.OnScoreAdded += ShowScore;
    private void OnDestroy() => _scoreService.OnScoreAdded -= ShowScore;

    [Inject]
    public void Construct(ScoreService scoreService) => _scoreService = scoreService;

    private void ShowScore(int value)
    {
        var element = _pool.GetFreeElement();
        element.Setup(value);
    }
}
