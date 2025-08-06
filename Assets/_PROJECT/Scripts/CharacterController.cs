using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _PROJECT.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        private CharacterInput characterInputMap;
        private Rigidbody characterRigidbody;
        [SerializeField] private float speed;
        private Vector3 _movementVector;

        private void Awake()
        {
            characterInputMap = new CharacterInput();
            characterInputMap.Enable();
            characterInputMap.PlayerMap.Jump.performed += OnJump;
            characterInputMap.PlayerMap.Jump.canceled -= OnJump;
            characterInputMap.PlayerMap.Pause.performed += OnPause;
            characterInputMap.PlayerMap.Pause.canceled -= OnPause;
            characterInputMap.PlayerMap.Interact.performed += OnInteract;
            characterInputMap.PlayerMap.Interact.canceled -= OnInteract;
            characterInputMap.PlayerMap.Movement.performed += x => OnPlayerMove(x.ReadValue<Vector2>());
            characterInputMap.PlayerMap.Movement.canceled += x => OnPlayerStopMove(x.ReadValue<Vector2>());
            characterInputMap.PlayerMap.Shoot.performed += OnShoot;
            characterInputMap.PlayerMap.Shoot.canceled -= OnShoot;

            if (characterRigidbody == null)
            {
                characterRigidbody = GetComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            characterRigidbody.linearVelocity = _movementVector * speed;
        }

        private void OnDisable()
        {
            characterInputMap.PlayerMap.Movement.performed -= x => OnPlayerMove(x.ReadValue<Vector2>());
            characterInputMap.PlayerMap.Movement.canceled -= x => OnPlayerStopMove(x.ReadValue<Vector2>());
        }

        private void OnInteract(InputAction.CallbackContext obj)
        {
            Debug.Log("We performed an interaction");
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log("We performed a jump");
        }
        
        private void OnPlayerMove(Vector2 incomingVector)
        {
            Debug.Log($"We are moving in direction {incomingVector}");
            _movementVector = new Vector3(incomingVector.x, characterRigidbody.linearVelocity.y, incomingVector.y);
        }

        private void OnPlayerStopMove(Vector2 incomingVector)
        {
            _movementVector = new Vector3(0, characterRigidbody.linearVelocity.y, 0);
        }
        
        private void OnPause(InputAction.CallbackContext obj)
        {
            Debug.Log("We performed a pause");
        }
        
        private void OnShoot(InputAction.CallbackContext obj)
        {
            Debug.Log("We performed a shot");
        }
    }
}
