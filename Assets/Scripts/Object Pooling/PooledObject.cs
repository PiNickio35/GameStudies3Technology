using UnityEngine;

namespace _PROJECT.Scripts.Object_Pooling
{
    public class PooledObject : MonoBehaviour
    {
        [SerializeField] protected int poolSize = 20;
        
        public int PoolSize => poolSize;
        public GameObject GameObject => this.gameObject;
        public PooledObject KeyRef;

        protected void ReturnToPool()
        {
            PoolManager.instance.ReturnToPool(this, KeyRef);
        }

        protected void Spawn(Vector3 position, Quaternion rotation)
        {
            PoolManager.instance.Spawn(this, position, rotation);
        }
    }
}
