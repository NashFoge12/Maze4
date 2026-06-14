using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;

    public GameObject[] hearts;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER");
    }
}