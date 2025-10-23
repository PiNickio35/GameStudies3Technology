using UnityEngine;
using UnityEngine.AI;

namespace _PROJECT.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Animal : MonoBehaviour, IDamageable
    {
        private int _health;
        protected Animator animator;
        protected NavMeshAgent agent;
        
        public virtual int Health
        {
            get => _health; 
            set => _health = value;
        }
        public virtual void Damage()
        {
            throw new System.NotImplementedException();
        }
    }
}
