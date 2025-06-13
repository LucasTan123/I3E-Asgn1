using UnityEngine;

public class RecoveryBehaviour : MonoBehaviour
{
    [SerializeField]
    int healAmount = 100;

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
