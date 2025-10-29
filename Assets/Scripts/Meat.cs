using UnityEngine;

public class Meat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hierso");
        if (other.tag == "Player")
        {
            PlayerController.Instance.meatCount.meatCount++;
            Destroy(gameObject);
        }
    }
}
