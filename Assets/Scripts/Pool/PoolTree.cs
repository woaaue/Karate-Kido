using UnityEngine;
using System.Collections.Generic;

public sealed class PoolTree : PoolMono<Tree>
{
    [SerializeField] private Tree _treePrefab;
    [SerializeField] private int _count;
    [SerializeField] private Transform _parent;

    private void Start() => Initialize(_treePrefab, _parent, _count);

    public List<Tree> GetPool()
    {
        return _pool;
    }
}
