using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    // Reference to the slider UI element for health
    public Slider healthBarSlider;

    // Reference to the text showing health values
    public TextMeshProUGUI healthBarValueText;

    // Method to update the health bar and text display
    public void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        // Show current and max health in the text (e.g. "75/100")
        healthBarValueText.text = currentHealth.ToString() + "/" + maxHealth.ToString();

        // Set the slider's max value (e.g. 100)
        healthBarSlider.maxValue = maxHealth;

        // Set the slider's current value (e.g. 75)
        healthBarSlider.value = currentHealth;
    }
}
