using Zenject;
using UnityEngine;
using JetBrains.Annotations;

public sealed class GameConditionService : MonoBehaviour
{
    private TreeService _treeService;
    private ScoreService _scoreService;
    private TimerService _timerService;

    [Inject]
    public void Construct(ScoreService scoreService, TimerService timerService, TreeService treeService)
    {
        _treeService = treeService;
        _scoreService = scoreService;
        _timerService = timerService;
    }

    [UsedImplicitly]
    public void RestartGame()
    {
        _treeService.SkipTree();
        _scoreService.ResetScore();
        _timerService.StartTimer();
    }

    [UsedImplicitly]
    public void StartGame()
    {
        _timerService.StartTimer();
    }
}
