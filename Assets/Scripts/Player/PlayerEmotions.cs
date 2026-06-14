using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEmotions : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 4;
    public int health;

    [Header("Emociones (0 - 100)")]
    public float fear;
    public float stress;
    public float rage;

    [Header("UI")]
    public TextMeshProUGUI healthText;
    public Slider fearBar;
    public Slider stressBar;
    public Slider rageBar;

    void Start()
    {
        health = maxHealth;
        UpdateUI();
    }

    void Update()
    {
        PassiveIncrease();
        CheckGameOver();
        UpdateUI();
    }

    void PassiveIncrease()
    {
        // ejemplo base (luego lo conectamos a Eidolon)
        fear += Time.deltaTime * 0.5f;
        stress += Time.deltaTime * 0.3f;
        rage += Time.deltaTime * 0.2f;
    }

    public void UsePill()
    {
        fear -= 20;
        stress -= 20;
        rage -= 10;

        ClampValues();
    }

    void ClampValues()
    {
        fear = Mathf.Clamp(fear, 0, 100);
        stress = Mathf.Clamp(stress, 0, 100);
        rage = Mathf.Clamp(rage, 0, 100);
    }

    void UpdateUI()
    {
        healthText.text = "VIDA: " + health;

        fearBar.value = fear;
        stressBar.value = stress;
        rageBar.value = rage;
    }

    void CheckGameOver()
    {
        if (fear >= 100 || stress >= 100 || rage >= 100)
        {
            Debug.Log("GAME OVER - mente colapsada");
        }
    }
}