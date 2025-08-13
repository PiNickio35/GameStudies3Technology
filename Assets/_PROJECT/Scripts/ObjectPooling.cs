using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _PROJECT.Scripts
{
    public class ObjectPooling : MonoBehaviour
    {
        [FormerlySerializedAs("_objectPool")] [SerializeField] private List<GameObject> objectPool = new List<GameObject>();
        [SerializeField] private GameObject objectPoolingPrefab;
        [SerializeField] private Transform objectPoolingParent;
        [SerializeField] private int objectPoolingAmount;

        private void Start()
        {
            for (int i = 0; i < objectPoolingAmount; i++)
            {
                objectPool.Add(Instantiate(objectPoolingPrefab, objectPoolingParent));
                if (objectPool[i].TryGetComponent<PooledObject>(out PooledObject pooledObject))
                {
                    pooledObject.SetObjectPoolingParent(this);
                }
                objectPool[i].SetActive(false);
            }
        }

        public void EnableObject()
        {
            GameObject tempGameObject = GetPooledObject();
            if (tempGameObject) tempGameObject.SetActive(true);
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    return objectPool[i];
                }
            }
            return null;
        }
    }
}
