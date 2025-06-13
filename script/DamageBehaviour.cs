using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    [SerializeField]
    int DamageAmount = -50;

    public void DealDamage(PlayerBehaviour player)
    {
        if (player != null)
        {
            player.ModifyHealth(DamageAmount);
        }
        else
        {
            Debug.LogWarning("PlayerBehaviour is null. Cannot deal damage.");
        }
    }
}
