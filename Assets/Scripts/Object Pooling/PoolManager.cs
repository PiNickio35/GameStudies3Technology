using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.Object_Pooling
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;
        
        private Dictionary<PooledObject, PoolHandler> _poolDictionary = new Dictionary<PooledObject, PoolHandler>();

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }

        public PooledObject Spawn(PooledObject prefab, Vector3 position, Quaternion rotation)
        {
            PooledObject itemFetched = _poolDictionary[prefab].GetFromPool();
            GameObject objectFetched = itemFetched.GameObject;
            objectFetched.SetActive(true);
            objectFetched.transform.position = position;
            objectFetched.transform.rotation = rotation;
            return itemFetched;
        }

        public void ReturnToPool(PooledObject prefab, PooledObject keyRef)
        {
            if (!_poolDictionary.ContainsKey(keyRef))
            {
                Debug.LogError("Trying to return an object outside the pool");
                //return;
            }
            _poolDictionary[keyRef].AddToPool(prefab);
            prefab.gameObject.SetActive(false);
        }

        public void InitQueue(PooledObject prefab)
        {
            _poolDictionary.Add(prefab, new PoolHandler(prefab));
            AddToQueue(prefab, prefab.PoolSize);
        }

        private void AddToQueue(PooledObject prefab, int poolSize)
        {
            for (int i = 0; i < poolSize; i++)
            {
                PooledObject createdObject = Instantiate(prefab.GameObject, this.gameObject.transform).GetComponent<PooledObject>();
                createdObject.gameObject.SetActive(false);
                _poolDictionary[prefab].AddToPool(createdObject);
            }
        }
    }
}
