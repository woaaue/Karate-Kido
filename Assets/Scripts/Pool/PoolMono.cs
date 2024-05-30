using UnityEngine;
using System.Collections.Generic;

public abstract class PoolMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private T _prefab;
    private Transform _container;

    private List<T> _pool;

    private protected void Initialize(T prefab, Transform container, int count)
    { 
        _prefab = prefab;
        _container = container;

        CreatePool(count);
    }

    private void CreatePool(int poolCount)
    {
        _pool = new List<T>();

        for (int i = 0; i < poolCount; ++i)
        {
           CreateObject();
        }
    }

    private void CreateObject(bool isActive = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container, false);
        
        createdObject.gameObject.SetActive(isActive);
        _pool.Add(createdObject);
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                element.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out T element))
            return element;

        throw new System.Exception($"There is no free element type {typeof(T)}");
    }
}
