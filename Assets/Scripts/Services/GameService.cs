using System;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TreeService _treeService;

    public event Action OnPlayerDied;
    public event Action OnPlayerHited;
    public event Action<string> OnGameEnded;
    public event Action<string, float> OnTreeAnimationRequested;

    private void Start() => _player.OnPlayerMoved += WentPlayer;
    private void OnDestroy() => _player.OnPlayerMoved -= WentPlayer;

    private void WentPlayer(Vector2 playerPosition)
    {
        if (!VerifyDeath(playerPosition))
        {
            OnPlayerHited?.Invoke();
            OnTreeAnimationRequested?.Invoke(_treeService.GetCurrentTree().Id, playerPosition.x);
            _treeService.EditQueue();
        }
        
        if (VerifyDeath(playerPosition))
        {
            OnPlayerDied?.Invoke();
            OnGameEnded?.Invoke("Player died");
        }
    }

    private bool VerifyDeath(Vector2 playerPosition)
    {
        var tree = _treeService.GetCurrentTree();

        bool isDeath = (playerPosition.x < 0 && tree.Type == ETreeType.Left
            || playerPosition.x > 0 && tree.Type == ETreeType.Right);
        
        return isDeath;
    }
}
