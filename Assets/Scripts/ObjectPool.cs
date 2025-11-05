using System.Collections.Generic;

using UnityEngine;

public class ObjectPool<T> where T : Component

{
    private readonly Stack<T> _pool = new Stack<T>();

    private readonly T _prefab;

    private readonly Transform _root;

    public ObjectPool(T prefab, Transform root, int prewarm = 0)

    {

        _prefab = prefab; _root = root;

        // Lazy by default. Prewarm only if explicitly requested.
        for (int i = 0; i < prewarm; i++)

        {

            var c = GameObject.Instantiate(_prefab, _root);

            c.gameObject.SetActive(false);

            _pool.Push(c);

        }

    }

    public T Get()

    {

        if (_pool.Count > 0)

        {

            var c = _pool.Pop();

            c.gameObject.SetActive(true);

            return c;

        }

        return GameObject.Instantiate(_prefab, _root);

    }

    public void Release(T obj)

    {

        obj.gameObject.SetActive(false);

        obj.transform.SetParent(_root, false);

        _pool.Push(obj);

    }

}