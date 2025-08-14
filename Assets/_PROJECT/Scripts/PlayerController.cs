using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _PROJECT.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _characterController;
        private Vector2 _moveInput;
        private Vector3 _velocity;
        [SerializeField] private CinemachinePanTilt playerCamera;
        [SerializeField] private float speed = 5.00f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravity = -9.81f;
        

        private void Awake()
        {
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
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && _characterController.isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            Application.Quit();
        }
    }
}
