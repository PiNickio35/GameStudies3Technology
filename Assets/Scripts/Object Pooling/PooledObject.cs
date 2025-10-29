using UnityEngine;

namespace Object_Pooling
{
    public class PooledObject : MonoBehaviour
    {
        [SerializeField] protected int poolSize = 20;
        
        public int PoolSize => poolSize;
        public GameObject GameObject => this.gameObject;
        public PooledObject KeyRef;

        protected void ReturnToPool()
        {
            PoolManager.Instance.ReturnToPool(this, KeyRef);
        }

        protected void Spawn(Vector3 position, Quaternion rotation)
        {
            PoolManager.Instance.Spawn(this, position, rotation);
        }
    }
}
