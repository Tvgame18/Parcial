using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField]
    private float maxHealth = 100f;

    private float currentHealth;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        currentHealth -= damage;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        currentHealth = 0f;
    }
}
