using UnityEngine;

namespace _PROJECT.Scripts
{
    public class DamageDealer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController.Instance.playerHealth.health--;
                PlayerController.Instance.particleSystem.Play();
            }
        }
    }
}
