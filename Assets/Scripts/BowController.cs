using Object_Pooling;
using UnityEngine;
using UnityEngine.InputSystem;

public class BowController : MonoBehaviour
{
    private Animator _bowAnimator; 
    private AudioSource _audioSource;
    private bool _isDrawing;

    [SerializeField] private PooledObject arrowPrefab; // The loaded arrow prefab
    public Transform spawnPosition;

    public float shootingForce = 100;

    private void Awake()
    {
        _bowAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PoolManager.Instance.InitQueue(arrowPrefab);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _isDrawing = context.action.IsPressed();
        if (_isDrawing)
        {
            _bowAnimator.SetBool("IsDrawing", true); // Set the Animator parameter
        }
        else
        {
            _bowAnimator.SetBool("IsDrawing", false); // Set the Animator parameter
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (_isDrawing)
        {
            ReleaseArrow();
        }
    }

    private void ReleaseArrow()
    {
        if (!_isDrawing) return;

        _isDrawing = false;
        _bowAnimator.SetBool("IsDrawing", false);
        _audioSource.Play();

        ShootArrow();
    }

    private void ShootArrow()
    {
        Vector3 shootingDirection = CalculateDirection().normalized;

        // Instantiate the arrow
        var arrow = PoolManager.Instance.Spawn(arrowPrefab, spawnPosition.position, Quaternion.identity);

        // Pointing the arrow to face the shooting direction
        arrow.transform.forward = shootingDirection;

        // Shoot the arrow
        arrow.GetComponent<Rigidbody>().AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
    }

    private Vector3 CalculateDirection()
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