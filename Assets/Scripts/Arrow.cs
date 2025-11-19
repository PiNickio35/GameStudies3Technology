using System.Collections;
using Object_Pooling;
using UnityEngine;

public class Arrow : PooledObject
{
    private Rigidbody _rb;
    private AudioSource _audioSource;
    [SerializeField] private float lifeSpan;

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterLifeSpan());
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // This method is called when the arrow hits a collider
    private void OnCollisionEnter(Collision collision)
    {
        StopCoroutine(DeactivateAfterLifeSpan());
        // Check if the arrow is not already stuck
        if (!collision.transform.CompareTag("Player"))
        {
            // Stop the movement by setting the Rigidbody's velocity and angular velocity to zero
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            // Optionally, freeze the Rigidbody's movement and rotation completely
            _rb.isKinematic = true;
            
            // Do damage
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                _audioSource.Play();
                collision.gameObject.GetComponent<IDamageable>().Damage();
            }
            
            Invoke(nameof(ReturnToPool), lifeSpan);
        }
    }

    private IEnumerator DeactivateAfterLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        ReturnToPool();
    }
}