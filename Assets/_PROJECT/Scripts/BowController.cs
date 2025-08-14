using UnityEngine;
using UnityEngine.InputSystem;

namespace _PROJECT.Scripts
{
    public class BowController : MonoBehaviour
    {
        // private Animator bowAnimator; // Reference to the Animator
        public string arrowItemName = "Arrow"; // Name of the arrow item in the inventory
        private bool isDrawing = false;

        public string arrowPrefabPath = "Arrow"; // Path in the Resources folder (without file extension)

        private GameObject arrowPrefab; // The loaded arrow prefab
        public Transform spawnPosition;

        public float shootingForce = 100;


        private void Start()
        {
            // bowAnimator = GetComponent<Animator>();
            LoadArrowPrefab();
        }
        private void LoadArrowPrefab()
        {
            // Load the arrow prefab from the specified path in the Resources folder
            arrowPrefab = Resources.Load<GameObject>(arrowPrefabPath);

            if (arrowPrefab == null)
            {
                Debug.LogError("Arrow prefab not found at path: " + arrowPrefabPath);
            }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            isDrawing = context.action.IsPressed();
            if (isDrawing)
            {
                // bowAnimator.SetBool("IsDrawing", true); // Set the Animator parameter
            }
            else
            {
                // bowAnimator.SetBool("IsDrawing", false); // Set the Animator parameter
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (isDrawing)
            {
                ReleaseArrow();
            }
        }

        private void ReleaseArrow()
        {
            if (!isDrawing) return;

            isDrawing = false;
            // bowAnimator.SetBool("IsDrawing", false);

            // Reduce the arrow count in the inventory
            // inventory.RemoveItem(arrowItemName, 1);

            // Call your shooting logic here
            ShootArrow();
        }

        private void ShootArrow()
        {
            Vector3 shootingDirection = CalculateDirection().normalized;

            // Instantiate the bullet
            GameObject arrow = Instantiate(arrowPrefab, spawnPosition.position, Quaternion.identity);
            arrow.transform.SetParent(null);

            // Poiting the bullet to face the shooting direction
            arrow.transform.forward = shootingDirection;

            // Shoot the bullet
            arrow.GetComponent<Rigidbody>().AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
        }

        public Vector3 CalculateDirection()
        {
            // Shooting from the middle of the screen to check where are we pointing at
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                // Hitting Something
                targetPoint = hit.point;
            }
            else
            {
                // Shooting at the air
                targetPoint = ray.GetPoint(100);
            } 

            // Returning the shooting direction and spread
            return targetPoint - spawnPosition.position;
        }

    }
}
