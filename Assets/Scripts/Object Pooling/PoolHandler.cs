using System.Collections.Generic;

namespace _PROJECT.Scripts.Object_Pooling
{
    public class PoolHandler
    {
        private PooledObject _prefab;
        public Queue<PooledObject> Poolables;

        public PoolHandler(PooledObject prefab)
        {
            _prefab = prefab;
            Poolables = new Queue<PooledObject>();
        }

        public void AddToPool(PooledObject poolable)
        {
            if (poolable.KeyRef == null)
            {
                poolable.KeyRef = _prefab;
            }
            Poolables.Enqueue(poolable);
        }

        public PooledObject GetFromPool()
        {
            return Poolables.Dequeue();
        }
    }
}
