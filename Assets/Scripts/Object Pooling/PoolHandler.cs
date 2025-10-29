using System.Collections.Generic;

namespace Object_Pooling
{
    public class PoolHandler
    {
        private PooledObject _prefab;
        public Queue<PooledObject> poolables;

        public PoolHandler(PooledObject prefab)
        {
            _prefab = prefab;
            poolables = new Queue<PooledObject>();
        }

        public void AddToPool(PooledObject poolable)
        {
            if (poolable.KeyRef == null)
            {
                poolable.KeyRef = _prefab;
            }
            poolables.Enqueue(poolable);
        }

        public PooledObject GetFromPool()
        {
            return poolables.Dequeue();
        }
    }
}
