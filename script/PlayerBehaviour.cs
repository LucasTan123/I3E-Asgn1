using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    // Player stats and state variables
    int maxHealth = 100;
    int currentHealth = 100;
    int currentScore = 0;
    int totalCollectiblesCollected = 0;
    int totalCollectiblesInLevel = 10;

    // Interaction and status flags
    bool canInteract = false;
    bool isRespawning = false;
    bool inDamageZone = false;

    // Damage timing
    float damageCooldown = 1f;
    float damageTimer = 0f;

    // References to current interactable objects
    GemBehaviour currentGem = null;
    CoinBehaviour currentCoin = null;
    DoorBehaviour currentDoor = null;

    // Serialized fields for projectiles, UI, and sounds
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float firestrength = 0f;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI collectiblesText;
    [SerializeField] GameObject winText;

    [SerializeField] HealthBarBehaviour healthBarScript;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip doorInteractSound;

    // Player respawn location
    Vector3 respawnPoint;

    void Start()
    {
        respawnPoint = transform.position;               // Set initial respawn position to player's start position
        UpdateHealthDisplay();                            // Update health UI on start
        UpdateScoreDisplay();                             // Update score UI on start
        UpdateCollectiblesDisplay();                      // Update collectibles UI on start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))                 // Check if player presses 'E' to interact
        {
            OnInteract();                                 // Attempt interaction
        }

        if (inDamageZone && !isRespawning)               // If player is in damage zone and not respawning
        {
            damageTimer -= Time.deltaTime;                // Decrease damage timer
            if (damageTimer <= 0f)                        // If timer elapsed, apply damage
            {
                ModifyHealth(-20);                         // Damage player health
                damageTimer = damageCooldown;             // Reset damage cooldown timer
            }
        }
    }

    void OnFire()
    {
        // Spawn and shoot a projectile with force from spawnPoint
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * firestrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }

    void OnInteract()
    {
        if (!canInteract) return;                         // Exit if interaction not possible

        if (currentCoin != null)                          // If near a coin, collect it
        {
            currentCoin.Collect(this);
            currentCoin = null;
            canInteract = false;
        }
        else if (currentGem != null)                      // If near a gem, collect it
        {
            currentGem.Collect(this);
            currentGem = null;
            canInteract = false;
        }
        else if (currentDoor != null)                     // If near a door, interact with it
        {
            currentDoor.Interact();
            if (audioSource != null && doorInteractSound != null)   // Play door sound
            {
                audioSource.PlayOneShot(doorInteractSound);
            }
            currentDoor = null;
            canInteract = false;
        }
    }

    public void ModifyScore(int amt)
    {
        currentScore += amt;                              // Add amount to score
        UpdateScoreDisplay();                             // Update score UI
    }

    public void ModifyHealth(int amount)
    {
        if (isRespawning) return;                         // Ignore health changes if respawning

        currentHealth += amount;                          // Change health by amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp health between 0 and maxHealth
        UpdateHealthDisplay();                            // Update health UI

        if (currentHealth <= 0 && !isRespawning)         // If health depleted, start respawn
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    void UpdateHealthDisplay()
    {
        if (healthBarScript != null)                      // Update health bar visual if assigned
        {
            healthBarScript.UpdateHealthDisplay(currentHealth, maxHealth);
        }

        if (healthText != null)                           // Update health text UI
        {
            healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        }
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)                            // Update score text UI
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    void UpdateCollectiblesDisplay()
    {
        if (collectiblesText != null)                     // Update collectible count UI
        {
            collectiblesText.text = "Collectibles: " + totalCollectiblesCollected + "/" + totalCollectiblesInLevel;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))              // When entering coin collectible trigger
        {
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
        }
        else if (other.CompareTag("Collectable"))         // When entering gem collectible trigger
        {
            canInteract = true;
            currentGem = other.GetComponent<GemBehaviour>();
        }
        else if (other.CompareTag("Door"))                 // When entering door trigger
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
        else if (other.CompareTag("Damage"))               // Enter damage zone, start taking damage
        {
            inDamageZone = true;
            damageTimer = 0f;                              // Immediate damage on entering
        }
        else if (other.CompareTag("Recovery"))             // Enter healing pad trigger
        {
            RecoveryBehaviour recoveryPad = other.GetComponent<RecoveryBehaviour>();
            if (recoveryPad != null)
            {
                recoveryPad.RecoverHealth(this);           // Heal player
                SetRespawnPoint(other.transform.position); // Update respawn location
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentCoin != null && other.gameObject == currentCoin.gameObject)  // Exiting coin trigger disables interaction
        {
            currentCoin = null;
            canInteract = false;
        }
        else if (currentGem != null && other.gameObject == currentGem.gameObject) // Exiting gem trigger disables interaction
        {
            currentGem = null;
            canInteract = false;
        }
        else if (currentDoor != null && other.gameObject == currentDoor.gameObject) // Exiting door trigger disables interaction
        {
            currentDoor = null;
            canInteract = false;
        }

        if (other.CompareTag("Damage"))                     // Exiting damage zone stops damage
        {
            inDamageZone = false;
        }
    }

    public void IncrementCollectibleCount()
    {
        totalCollectiblesCollected++;                         // Increase collectible count
        UpdateCollectiblesDisplay();                          // Update collectible UI

        if (totalCollectiblesCollected >= totalCollectiblesInLevel && winText != null) // Show win text if all collected
        {
            winText.SetActive(true);
        }
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;                       // Update respawn point position
    }

    System.Collections.IEnumerator RespawnRoutine()
    {
        isRespawning = true;                                  // Disable damage and interactions

        yield return new WaitForSeconds(0.1f);                // Short delay before respawn
        Debug.Log("Teleporting to respawn point: " + respawnPoint);
        transform.position = respawnPoint;                    // Move player to respawn position

        currentHealth = maxHealth;                             // Restore health
        UpdateHealthDisplay();                                 // Update health UI

        yield return new WaitForSeconds(0.2f);                // Short delay after respawn
        isRespawning = false;                                  // Enable damage and interactions again
    }
}
