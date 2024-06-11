using System;
using Zenject;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TreeService _treeService;

    public event Action OnPlayerHit;
    public event Action OnPlayerDied;
    public event Action<Tree, float> OnTreeAnimationRequested;

    private Score _scoreSystem;
    private LevelSystem _levelSystem;

    [Inject]
    public void Construct(Score score, LevelSystem levelSystem)
    {
        _scoreSystem = score;
        _levelSystem = levelSystem;
    }

    private void Start()
    {
        _player.OnPlayerMoved += WentPlayer;
    }

    private void OnDestroy()
    {
        _player.OnPlayerMoved -= WentPlayer;
    }

    private void WentPlayer(Vector2 playerPosition)
    {
        if (!VerifyDeath(playerPosition))
        {
            OnTreeAnimationRequested?.Invoke(_treeService.GetCurrentTree(), playerPosition.x);
            OnPlayerHit?.Invoke();
            _treeService.EditQueue();
        }
    }

    private bool VerifyDeath(Vector2 playerPosition)
    {
        var tree = _treeService.GetCurrentTree();

        if (playerPosition == Vector2.left && tree.Type == ETreeType.Left || 
            playerPosition == Vector2.right && tree.Type == ETreeType.Right || 
            tree.Type == ETreeType.None)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
}
