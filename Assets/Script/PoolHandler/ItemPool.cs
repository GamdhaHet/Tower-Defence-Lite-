using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Pool
{
    [System.Serializable]
    public class ItemPool
    {
        [SerializeField] private MonoBehaviour _itemPrefab;
        [SerializeField] private int _initialPoolSize = 5;
        [SerializeField] private int _regeneratePoolSize = 0;

        [ShowInInspector] private Queue<MonoBehaviour> _itemQueue;
        private Transform _parentTransform;

        public void InitializeItemPool(Transform parentTransform)
        {
            _parentTransform = parentTransform;
            _itemQueue = new Queue<MonoBehaviour>();
            RegenerateItems(_initialPoolSize);
        }

        public MonoBehaviour GetOrCreateItem()
        {
            if (_itemQueue.Count == 0)
            {
                if (_regeneratePoolSize > 0)
                    RegenerateItems(_regeneratePoolSize);
                else
                    return null;
            }
            MonoBehaviour pooledItem = _itemQueue.Dequeue();
            pooledItem.gameObject.SetActive(true);
            return pooledItem;
        }

        public void ReturnItemToPool(MonoBehaviour item)
        {
            item.transform.SetParent(_parentTransform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = Vector3.one;
            item.gameObject.SetActive(false);
            _itemQueue.Enqueue(item);
        }

        private void RegenerateItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                MonoBehaviour newItem = Object.Instantiate(_itemPrefab, _parentTransform);
                newItem.gameObject.SetActive(false);
                _itemQueue.Enqueue(newItem);
            }
        }
    }
}
