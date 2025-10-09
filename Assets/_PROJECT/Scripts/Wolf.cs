using UnityEngine;
using UnityEngine.AI;

namespace _PROJECT.Scripts
{
    public class Wolf : Animal
    {
        public Transform target;
        public float attackDistance;
    
        private float m_Distance;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            Health = 2;
        }

        private void Update()
        {
            if (GameController.Instance.state == GameState.Explore)
            {
                m_Distance = Vector3.Distance(agent.transform.position, target.position);
                if (m_Distance < attackDistance)
                {
                    agent.isStopped = true;
                    animator.SetBool("Attack", true);
                }
                else
                {
                    agent.isStopped = false;
                    animator.SetBool("Attack", false);
                    agent.destination = target.position;
                }
            }
        }

        public override void Damage()
        {
            Health--;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
