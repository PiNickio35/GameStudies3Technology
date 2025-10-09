using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _PROJECT.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        [SerializeField] private PlayerHealth playerHealth;
        private CharacterController _characterController;
        private Vector2 _moveInput;
        private Vector3 _velocity;
        [SerializeField] private CinemachinePanTilt playerCamera;
        [SerializeField] private float speed = 5.00f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravity = -9.81f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var move = new Vector3(_moveInput.x, 0, _moveInput.y);
            // Take the pan angle from the CineMachinePanTilt component
            var panAngle = playerCamera.PanAxis.Value;
            var panRotation = Quaternion.Euler(0, panAngle, 0);

            // Rotate the movement input based on the pan angle
            var moveDirection = panRotation * move;

            // Move the player, and update the direction they're facing
            _characterController.Move(moveDirection * (speed * Time.deltaTime));
            transform.localRotation = panRotation;
        }

        private void FixedUpdate()
        {
            _velocity.y += gravity * Time.fixedDeltaTime;
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (GameController.Instance.state != GameState.Explore) return;
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (GameController.Instance.state != GameState.Explore) return;
            if (context.performed && _characterController.isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            switch (GameController.Instance.state)
            {
                case GameState.Explore:
                    GameController.Instance.Pause();
                    break;
                case GameState.Paused:
                    GameController.Instance.UnPause();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
