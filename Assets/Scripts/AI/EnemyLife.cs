using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] float _maxHealth = 50f;
    [SerializeField] private float currentHealth;

    [SerializeField] HealthBar HealthBar;
    [SerializeField] PlayerLife PlayerLife;
    void Start()
    {
        currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        HealthBar.UpdateHealthBar(_maxHealth , currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        PlayerLife.NumberEnemy++;
        PlayerLife.CalculateScoreEnemy();
    }
}
