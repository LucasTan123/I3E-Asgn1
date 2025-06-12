using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    // Player's maximum health
    int maxHealth = 100;
    // Player's current health
    int currentHealth = 100;
    // Player's current score
    int currentScore = 0;
    // Flag to check if the player can interact with objects
    bool canInteract = false;
    // Stores the current coin object the player has detected
    GemBehaviour currentGem = null;
    CoinBehaviour currentCoin = null;
    RecoveryBehaviour currentRecovery;
    DamageBehaviour currentDamage;
    // Stores the current door object the player has detected
    DoorBehaviour currentDoor = null;
    [SerializeField]
    GameObject projectile;
    [SerializeField]    // The Interact callback for the Interact Input Action
    Transform spawnPoint;
    [SerializeField]
    float firestrength = 0f;
    [SerializeField]
    Text healthText;

    void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
        else
        {
            Debug.LogWarning("Health Text UI element is not assigned.");
        }
     }
    // This method is called when the player presses the interact button
    void OnInteract()
    {
        // Check if the player can interact with objects
        if (canInteract)
        {
            // Check if the player has detected a coin or a door
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                // Call the Collect method on the coin object
                // Pass the player object as an argument
                currentCoin.Collect(this);
            }
            else if (currentGem != null)
            {
                Debug.Log("Interacting with gem");
                // Call the Collect method on the gem object
                // Pass the player object as an argument
                currentGem.Collect(this);
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                // Call the Interact method on the door object
                // This allows the player to open or close the door
                currentDoor.Interact();
            }
        }
    }

    // Method to modify the player's score
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current score
    // The method is public so it can be accessed from other scripts
    public void ModifyScore(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentScore += amt;
    }

    // Method to modify the player's health
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current health
    // The method is public so it can be accessed from other scripts
    public void ModifyHealth(int amount)
    {
        // Check if the current health is less than the maximum health
        // If it is, increase the current health by the amount passed as an argument
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            // Check if the current health exceeds the maximum health
            // If it does, set the current health to the maximum health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        Debug.Log("Player health: " + currentHealth);
        UpdateHealthDisplay(); // Call the method to update the health displa
    }

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * firestrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }
    // Trigger Callback for when the player enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        // Check if the player detects a trigger collider tagged as "Collectible" or "Door"
        if (other.CompareTag("Collectible"))
        {
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
        }
        else if (other.CompareTag("Collectible"))
        {
            canInteract = true;
            currentGem = other.GetComponent<GemBehaviour>();
         }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
        else if (other.CompareTag("Recovery"))
        {
            Debug.Log("Recovering health");
            other.GetComponent<RecoveryBehaviour>().RecoverHealth(this);
        }
        else if (other.CompareTag("Damage"))
        {
            Debug.Log("Taking damage");
            other.GetComponent<DamageBehaviour>().DealDamage(this);
        }
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected coin or door
        if (currentCoin != null)
        {
            // If the object that exited the trigger is the same as the current coin
            if (other.gameObject == currentCoin.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current coin to null
                // This prevents the player from interacting with the coin
                canInteract = false;
                currentCoin = null;
            }
        }
        else if (currentGem != null && other.gameObject == currentGem.gameObject)
        {
            // If the object that exited the trigger is the same as the current gem
            // Set the canInteract flag to false
            // Set the current gem to null
            // This prevents the player from interacting with the gem
            canInteract = false;
            currentGem = null;
        }
        else if (currentDoor != null)
        {
            // If the object that exited the trigger is the same as the current door
            if (other.gameObject == currentDoor.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current door to null
                // This prevents the player from interacting with the door
                canInteract = false;
                currentDoor = null;
            }
        }
        else if (currentRecovery != null)
        {
            // If the object that exited the trigger is the same as the current recovery object
            if (other.gameObject == currentRecovery.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current recovery to null
                // This prevents the player from interacting with the recovery object
                canInteract = false;
                currentRecovery = null;
            }
        }
        else if (currentDamage != null)
        {
            // If the object that exited the trigger is the same as the current damage object
            if (other.gameObject == currentDamage.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current damage to null
                // This prevents the player from interacting with the damage object
                canInteract = false;
                currentDamage = null;
            }
        }
    }
    void Start()
    {
        // Update the health display at the start
        UpdateHealthDisplay();
    }
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Collectible"))
            {
                if (currentCoin != null)
                {
                    //currentCoin.Unhighlight();
                }
                canInteract = true;
                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
                //currentCoin.Highlight();
            }
            else if (hitInfo.collider.CompareTag("Collectible"))
            {
                if (currentGem != null)
                {
                    //currentGem.Unhighlight();
                }
                canInteract = true;
                currentGem = hitInfo.collider.GetComponent<GemBehaviour>();
                //currentGem.Highlight();
            }
        }
        else if (currentCoin != null)
        {
            //currentCoin.Unhighlight();
            //currentCoin = null;
        }
    }
}