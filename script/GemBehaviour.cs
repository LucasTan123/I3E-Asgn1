using UnityEngine;

public class GemBehaviour : MonoBehaviour
{
    // The score value this gem gives the player
    [SerializeField] int gemValue = 20;

    // Called when the player collects the gem
    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(gemValue);              // Increase player's score
        player.IncrementCollectibleCount();        // Update collectible counter
        GetComponent<Collider>().enabled = false;  // Disable collider to prevent re-collection
        Destroy(gameObject);                       // Remove gem from the scene
    }

    void Update()
    {
        // Rotate gem continuously for visual effect
        transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);
    }
}
