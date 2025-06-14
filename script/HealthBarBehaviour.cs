using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarBehaviour : MonoBehaviour
{
    // UI slider for the health bar
    [SerializeField] private Slider healthBarSlider;

    // Text to display the current health value
    [SerializeField] private TextMeshProUGUI healthBarValueText;

    // Updates the health bar UI
    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        // Check if UI references are assigned
        if (healthBarSlider == null || healthBarValueText == null)
        {
            Debug.LogWarning("HealthBar UI references are missing. Please assign them in the Inspector.");
            return;
        }

        // Clamp health value to ensure it's within valid range
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar's max value and current value
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;

        // Update the health text to show current/max health
        healthBarValueText.text = $"{currentHealth}/{maxHealth}";
    }
}
