using Zenject;
using UnityEngine;

public sealed class ScoreViewerService : MonoBehaviour
{
    [SerializeField] private ScorePool _pool;

    [Inject] private ScoreService _scoreService;

    private void Start() => _scoreService.OnScoreAdded += ShowScore;
    private void OnDestroy() => _scoreService.OnScoreAdded -= ShowScore;

    private void ShowScore(int value)
    {
        var element = _pool.GetFreeElement();
        element.Setup(value);
    }
}
