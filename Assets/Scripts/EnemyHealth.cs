using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maximumHealth = 5f;
    [SerializeField] private bool canDrop = true;
    [SerializeField] private int dropChance = 70;

    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject destroyFX;
    [SerializeField] private GameObject healthDrop;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maximumHealth;
        healthBar.maxValue = maximumHealth;
        healthBar.value = currentHealth;
    }

    public void AddDamage(float damage)
    {
        currentHealth -= damage;
        if (!healthBar.IsActive()) healthBar.gameObject.SetActive(true);
        healthBar.value = currentHealth;
        if (currentHealth <= 0) MakeDead();
    }

    private void MakeDead()
    {
        Instantiate(destroyFX, transform.position, transform.rotation);

        if (canDrop && Random.Range(0, 100) < dropChance)
        {
            Instantiate(healthDrop, transform.position, transform.rotation);
        }

        if (gameObject.transform.parent != null) Destroy(gameObject.transform.parent.gameObject);
        else Destroy(gameObject);
    }
}
