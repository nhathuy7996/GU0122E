using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> : MonoBehaviour where T : Component
{
    private static ObjectPooling<T> _instance;
    public static ObjectPooling<T> instance => _instance;

    [SerializeField] T objectToPool;
    [SerializeField] int size;

    Stack<T> pools = new Stack<T>();
    T clone;
    private void Awake()
    {
        CreatePool();
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);
        } else if (this.GetInstanceID() != instance.GetInstanceID())
            Destroy(gameObject);
    }
    void CreatePool()
    {
        for (int i = 0; i < size; i++)
        {
            clone = Instantiate(objectToPool, transform);
            clone.gameObject.SetActive(false);
            pools.Push(clone);
        }
    }
    // 1 la false, set up chi so -> active true
    // 2: active object luon
    public T GetObjectFromPool()
    {
        if (pools.Count > 0)
            clone = pools.Pop();
        else
        {
            clone = Instantiate(objectToPool, transform);
            clone.gameObject.SetActive(false);
        }
        return clone;
    }
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pools.Push(obj);
    }
}
