using System.Collections;
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
        [SerializeField] private float spawnDelay;

        private void Start()
        {
            StartCoroutine(InstantiatePooledObject());
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

        IEnumerator InstantiatePooledObject()
        {
            yield return null;
            int objectCount = 0;
            while (objectCount < objectPoolingAmount)
            {
                if (objectPoolingParent)
                {
                    GameObject tempGameObject = Instantiate(objectPoolingPrefab, objectPoolingParent.transform);
                    tempGameObject.SetActive(false);
                    objectPool.Add(tempGameObject);
                }
                else
                {
                    GameObject tempGameObject = Instantiate(objectPoolingPrefab);
                    tempGameObject.GetComponent<PooledObject>().SetObjectPoolingParent(this);
                    tempGameObject.SetActive(false);
                    objectPool.Add(tempGameObject);
                }
                objectCount++;
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
