using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _PROJECT.Scripts
{
    public class Wolf : Animal
    {
        public enum AIState { Idle, Walking, Attacking, Running}
        public AIState currentState = AIState.Idle;
        public float walkingSpeed = 3.5f;
        public float runningSpeed = 7f;
        public Transform target;
        public float attackDistance;
        public int awarenessArea = 15;
        private SphereCollider c;
    
        private float _distance;
        private float _actionTimer = 0;
        private bool _switchAction = false;
        private List<Vector3> _previousIdlePoints = new List<Vector3>();

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 0;
            agent.autoBraking = true;
        
            c = gameObject.AddComponent<SphereCollider>();
            c.isTrigger = true;
            c.radius = awarenessArea;
        
            currentState = AIState.Idle;
            _actionTimer = Random.Range(0.1f, 2.0f);
            animator = GetComponent<Animator>();
            Health = 2;
        }

        private void Update()
        {
            if (GameController.Instance.state == GameState.Explore)
            {
                //Wait for the next course of action
                if (_actionTimer > 0)
                {
                    _actionTimer -= Time.deltaTime;
                }
                else
                {
                    _switchAction = true;
                }

                if (currentState == AIState.Idle)
                {
                    if (_switchAction)
                    {
                        if (target)
                        {
                            // Attack
                            agent.SetDestination(target.position);
                            currentState = AIState.Running;
                            SwitchAnimationState(currentState);
                        }
                        else
                        {
                            //No enemies nearby, start walking
                            _actionTimer = Random.Range(14, 22);

                            currentState = AIState.Walking;
                            SwitchAnimationState(currentState);

                            //Keep last 5 Idle positions for future reference
                            _previousIdlePoints.Add(transform.position);
                            if (_previousIdlePoints.Count > 5)
                            {
                                _previousIdlePoints.RemoveAt(0);
                            }
                        }
                    }
                }
                else if (currentState == AIState.Walking)
                {
                    //Set NavMesh Agent Speed
                    agent.speed = walkingSpeed;

                    // Check if we've reached the destination
                    if (DoneReachingDestination())
                    {
                        currentState = AIState.Idle;
                    }
                }
                else if (currentState == AIState.Running)
                {
                    _distance = Vector3.Distance(agent.transform.position, target.position);
                    //Set NavMesh Agent Speed
                    agent.speed = runningSpeed;

                    if (_distance >= awarenessArea)
                    {
                        target = null;
                        _actionTimer = Random.Range(1.4f, 3.4f);
                        currentState = AIState.Idle;
                        SwitchAnimationState(currentState);
                    }
                    else if (_distance < attackDistance)
                    {
                        currentState = AIState.Attacking;
                        SwitchAnimationState(currentState);
                    }
                }
                else if (currentState == AIState.Attacking)
                {
                    _distance = Vector3.Distance(agent.transform.position, target.position);
                    agent.speed = 0;
                    if (_distance >= awarenessArea)
                    {
                        target = null;
                        _actionTimer = Random.Range(1.4f, 3.4f);
                        currentState = AIState.Idle;
                        SwitchAnimationState(currentState);
                    }
                    else if (_distance > attackDistance)
                    {
                        currentState = AIState.Running;
                        SwitchAnimationState(currentState);
                    }
                    else
                    {
                        PlayerController.Instance.playerHealth.health -= 1;
                    }
                }
                _switchAction = false;
            }
        }

        private bool DoneReachingDestination()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //Done reaching the Destination
                        return true;
                    }
                }
            }

            return false;
        }

        private void SwitchAnimationState(AIState state)
        {
            //Animation control
            if (animator)
            {
                animator.SetBool("isAttacking", state == AIState.Attacking);
                animator.SetBool("isRunning", state == AIState.Running);
                animator.SetBool("isWalking", state == AIState.Walking);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Make sure the Player instance has a tag "Player"
            if (!other.CompareTag("Player"))
                return;

            target = other.transform;

            _actionTimer = Random.Range(0.24f, 0.8f);
            currentState = AIState.Idle;
            SwitchAnimationState(currentState);
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
