using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private float healAmmount = 3f;

    private bool pickedUp;

    private void Awake() => pickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.GetHealth(healAmmount);
            pickedUp = true;
            Destroy(gameObject);
        }
    }
}
