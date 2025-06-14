using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    // Value of the coin (how much score it gives)
    [SerializeField] int coinValue = 10;

    // Called when the player collects the coin
    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(coinValue);              // Add score to player
        player.IncrementCollectibleCount();         // Increase collectible count
        GetComponent<Collider>().enabled = false;   // Disable collider to prevent double collection
        Destroy(gameObject);                        // Remove the coin from the scene
    }

    void Update()
    {
        // Rotate the coin continuously for visual effect
        transform.Rotate(Vector3.left * 50 * Time.deltaTime, Space.Self);
    }
}
