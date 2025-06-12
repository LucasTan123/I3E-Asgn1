using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    // Amount of health to deduct
    [SerializeField]
    int DamageAmount = 50;

    // Method to deduct health
    // This method will be called when the player interacts with the damage object
    // It takes a PlayerBehaviour object as a parameter
    // This allows the recovery object to modify the player's health
    // The method is public so it can be accessed from other scripts
    public void DealDamage(PlayerBehaviour player)
    {
        if (player != null)
        {
            player.ModifyHealth(-DamageAmount);
        }
        else
        {
            Debug.LogWarning("PlayerBehaviour is null. Cannot deal damage.");
        }
    }
}
