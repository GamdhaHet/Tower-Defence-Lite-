using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Pool
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance { get; private set; }

        [System.Serializable]
        private class NamedPool
        {
            public string key;
            public ItemPool itemPool;
        }

        [SerializeField] private List<NamedPool> _pools = new();

        private Dictionary<string, ItemPool> _poolDictionary = new();
        private Dictionary<string, Transform> _poolParents = new();

        private void Awake()
        {
            Instance = this;
            InitializeAllPools();
        }

        private void InitializeAllPools()
        {
            foreach (NamedPool namedPool in _pools)
            {
                if (string.IsNullOrEmpty(namedPool.key))
                    continue;

                if (_poolDictionary.ContainsKey(namedPool.key))
                    continue;

                Transform parent = new GameObject($"Pool_{namedPool.key}").transform;
                parent.SetParent(transform);

                namedPool.itemPool.InitializeItemPool(parent);

                _poolDictionary[namedPool.key] = namedPool.itemPool;
                _poolParents[namedPool.key] = parent;
            }
        }

        public MonoBehaviour GetItem(string key)
        {
            if (!TryGetPool(key, out ItemPool pool)) return null;

            MonoBehaviour item = pool.GetOrCreateItem();
            return item;
        }

        public T GetItem<T>(string key) where T : MonoBehaviour
        {
            MonoBehaviour item = GetItem(key);
            if (item == null) return null;

            T typed = item as T;
            return typed;
        }

        public void ReturnItem(string key, MonoBehaviour item)
        {
            if (item == null)
                return;

            if (!TryGetPool(key, out ItemPool pool)) return;

            pool.ReturnItemToPool(item);
        }

        private bool TryGetPool(string key, out ItemPool pool)
        {
            if (_poolDictionary.TryGetValue(key, out pool)) return true;
            return false;
        }
    }
}