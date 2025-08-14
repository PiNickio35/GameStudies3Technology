using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private int arrowDamage = 25;
  
    private bool isStuck = false; // Flag to track if the arrow is stuck
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 5f); // Destroy the arrow after 5 seconds if it doesn't hit anything
    }

    // This method is called when the arrow hits a collider
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the arrow is not already stuck
        if (!isStuck && !collision.transform.CompareTag("Player"))
        {
            isStuck = true; // Mark the arrow as stuck

            // Stop the movement by setting the Rigidbody's velocity and angular velocity to zero
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Optionally, freeze the Rigidbody's movement and rotation completely
            rb.isKinematic = true;

            Debug.Log("Arrow stuck! :" + collision.transform.name);
        }

        // Do damage
    }
}
