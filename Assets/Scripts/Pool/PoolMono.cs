using UnityEngine;
using System.Collections.Generic;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public Transform Container { get; }

    private List<T> pool;

    public PoolMono(T prefab, Transform container, int count)
    { 
        Prefab = prefab;
        Container = container;

        CreatePool(count);
    }

    private void CreatePool(int poolCount)
    {
        pool = new List<T>();

        for (int i = 0; i < poolCount; ++i)
        {
           CreateObject();
        }
    }

    private void CreateObject(bool isActive = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container, false);
        
        createdObject.gameObject.SetActive(isActive);
        pool.Add(createdObject);
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var item in pool)
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
