using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.playerHealth.health--;
            PlayerController.Instance.GetComponent<ParticleSystem>().Play();
        }
    }
}