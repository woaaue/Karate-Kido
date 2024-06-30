using UnityEngine;
using System.Collections.Generic;

public sealed class ScorePool : PoolMono<ScoreViewer>
{
    [SerializeField] private int _count;
    [SerializeField] private Transform _parent;
    [SerializeField] private ScoreViewer _scorePrefab;

    private void Start() => Initialize(_scorePrefab, _parent, _count);

    public List<ScoreViewer> GetPool()
    {
        return _pool;
    }
}
