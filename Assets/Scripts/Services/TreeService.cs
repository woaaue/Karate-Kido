using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public sealed class TreeService : MonoBehaviour
{
    [SerializeField] private PoolTree _treePool;
    [SerializeField] private int _treeSceneStart;

    private int _treeOnScene;
    private ETreeType _lastType;
    private Vector2 _lastPosition;
    private List<ETreeType> _lastTreeTypes;

    private void Start()
    {
        _lastTreeTypes = new List<ETreeType>();

        Init(_treeSceneStart);
    }

    public void EditQueue()
    {
        ShiftPool();
        Init(1);
        _treePool.MoveToLast(_treePool.GetPool().First());
    }

    public void ScipTree()
    {
        _treePool.GetPool().First().gameObject.SetActive(false);
        _treePool.MoveToLast(_treePool.GetPool().First());
        ShiftPool();
        Init(1);
    }

    public Tree GetCurrentTree()
    {
        return _treePool.GetPool().First();
    }

    private void ShiftPool()
    {
        _treePool.GetPool()
            .Where(tree => tree.isActiveAndEnabled && !tree.IsActiveAnim)
            .ToList()
            .ForEach(tree =>
            {
                tree.SetupTransform(tree.transform.position.x, 
                    tree.transform.position.y - tree.transform.localScale.y);

                _lastPosition = tree.transform.position;
            });
    }

    private void Init(int value)
    {
        for (int counter = 0; counter < value; ++counter)
        {
            ActiveObject();
        }
    }

    private void ActiveObject()
    {
        var treeObject = _treePool.GetFreeElement();

        if (_lastPosition == Vector2.zero && _treeOnScene < 1)
        {
            treeObject.SetupTransform(0,0);
            _lastPosition = treeObject.transform.position;
        }
        else
        {
            treeObject.SetupTransform(_lastPosition.x, _lastPosition.y + treeObject.transform.localScale.y);
        }

        _lastPosition = treeObject.transform.position;
        treeObject.SetupType(SetTreeType());
        _treeOnScene += 1;
    }

    private ETreeType SetTreeType()
    {
        if (_treeOnScene < 1)
        {
            _lastType = ETreeType.None;
            _lastTreeTypes.Add(_lastType);
            return _lastType;
        }

        ETreeType newType;
        float random = Random.Range(0f, 1f);

        if (_lastTreeTypes.Count < 2)
        {
            newType = 
                random > 0.3f ? 
                (random > 0.65f ? ETreeType.Left : ETreeType.Right) : ETreeType.None;
        }
        else
        {
            ETreeType last1 = _lastTreeTypes[_lastTreeTypes.Count - 1];
            ETreeType last2 = _lastTreeTypes[_lastTreeTypes.Count - 2];

            if (last1 == ETreeType.None && last2 == ETreeType.None)
            {
                newType = 
                    random > 0.3f ? 
                    (random > 0.65f ? ETreeType.Left : ETreeType.Right) : ETreeType.None;
            }
            else if (last1 == ETreeType.Left && last2 == ETreeType.Left)
            {
                newType = ETreeType.None;
            }
            else if (last1 == ETreeType.Right && last2 == ETreeType.Right)
            {
                newType = ETreeType.None;
            }
            else if (last1 == ETreeType.None)
            {
                newType = 
                    random > 0.3f ? 
                    (last2 == ETreeType.Left ? ETreeType.Right : ETreeType.Left) : ETreeType.None;
            }
            else
            {
                newType = ETreeType.None;
            }
        }

        _lastTreeTypes.Add(newType);

        if (_lastTreeTypes.Count > 3)
        {
            _lastTreeTypes.RemoveAt(0);
        }

        _lastType = newType;
        return newType;
    }
}
