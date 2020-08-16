using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class MonoPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<T> objectPool;
    public T objectPrefab;

    private void Start()
    {
        objectPool = new List<T>();
    }

    public T GetFreeObject()
    {
        T freeObject = objectPool.Find(obj => obj.gameObject.activeInHierarchy == false);
        if (freeObject == null)
            freeObject = IncreasePool();

        freeObject.gameObject.SetActive(true);
        return freeObject;
    }

    public void SetObjectFree(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void Clear()
    {
        objectPool.ForEach(obj => { Destroy(obj); });
        objectPool.Clear();
    }

    private T IncreasePool()
    {
        T newObject = Instantiate(objectPrefab, transform);
        objectPool.Add(newObject);
        return newObject;
    }
}