using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Animal : MonoBehaviour, IDamageable
{
    private int _health;
    [SerializeField] protected GameObject meatPrefab;
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