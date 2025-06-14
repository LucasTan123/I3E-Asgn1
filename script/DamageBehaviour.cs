using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    // Amount of damage to apply (negative value)
    [SerializeField]
    int DamageAmount = -50;

    // Call this method to deal damage to the player
    public void DealDamage(PlayerBehaviour player)
    {
        if (player != null)
        {
            player.ModifyHealth(DamageAmount); // Reduce player's health
        }
        else
        {
            Debug.LogWarning("PlayerBehaviour is null. Cannot deal damage."); // Debug warning if player is missing
        }
    }
}