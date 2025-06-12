using UnityEngine;

public class GemBehaviour : MonoBehaviour
{
    // Coin value that will be added to the player's score
    MeshRenderer myMeshRenderer; // Reference to the MeshRenderer component for visual effects

    [SerializeField]
    //Material highlightMat; // Material to use when the coin is highlighted
    //Material originalMat; // Original material of the coin for unhighlighting
    int gemValue = 50;

    // Method to collect the coin
    // This method will be called when the player interacts with the coin
    // It takes a PlayerBehaviour object as a parameter
    // This allows the coin to modify the player's score
    // The method is public so it can be accessed from other scripts
    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Gem collected!");

        // Add the coin value to the player's score
        // This is done by calling the ModifyScore method on the player object
        // The coinValue is passed as an argument to the method
        // This allows the player to gain points when they collect the coin
        player.ModifyScore(gemValue);
        Destroy(gameObject); // Destroy the coin object
    }

    void Update()
    {
        // Rotate the coin around the X-axis for visual effect
        transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);
    }
}
