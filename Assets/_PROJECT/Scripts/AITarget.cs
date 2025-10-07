using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITarget : MonoBehaviour
{
    public Transform target;
    public float attackDistance;
    
    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private float m_Distance;
    
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        m_Distance = Vector3.Distance(m_Agent.transform.position, target.position);
        if (m_Distance < attackDistance)
        {
            m_Agent.isStopped = true;
            m_Animator.SetBool("Attack", true);
        }
        else
        {
            m_Agent.isStopped = false;
            m_Animator.SetBool("Attack", false);
            m_Agent.destination = target.position;
        }
        
    }
}
