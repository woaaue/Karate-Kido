using UnityEngine;

public sealed class PoolObjects : MonoBehaviour
{
    [SerializeField] private Tree _treePrefab;
    [SerializeField] private int _poolCount;

    private PoolMono<Tree> _treePool;

    private void Start()
    {
       _treePool = new PoolMono<Tree>(_treePrefab, gameObject.transform, _poolCount);
    }

    public Tree GetTree()
    {
        var element = _treePool.GetFreeElement();

        return element;
    }
}
