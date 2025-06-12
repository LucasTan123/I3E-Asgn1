using UnityEngine;

public class RecoveryBehaviour : MonoBehaviour
{
    // Amount of health to recover
    [SerializeField]
    int healAmount = 100;

    // Method to recover health
    // This method will be called when the player interacts with the recovery object
    // It takes a PlayerBehaviour object as a parameter
    // This allows the recovery object to modify the player's health
    // The method is public so it can be accessed from other scripts
    public void RecoverHealth(PlayerBehaviour player)
    {
        if (player != null)
        {
            player.ModifyHealth(healAmount);
        }
        else
        {
            Debug.LogWarning("PlayerBehaviour is null. Cannot recover health.");
        }
    }
}
