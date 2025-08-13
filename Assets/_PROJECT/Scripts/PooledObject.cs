using UnityEngine;

namespace _PROJECT.Scripts
{
    public class PooledObject : MonoBehaviour
    {
        private ObjectPooling _objectPoolingParent;

        public void SetObjectPoolingParent(ObjectPooling objectPoolingParent)
        {
            this._objectPoolingParent = objectPoolingParent;
        }
    }
}
