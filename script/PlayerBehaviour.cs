using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;
    int currentScore = 0;
    bool canInteract = false;
    GemBehaviour currentGem = null;
    CoinBehaviour currentCoin = null;
    RecoveryBehaviour currentRecovery;
    DamageBehaviour currentDamage;
    DoorBehaviour currentDoor = null;

    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    float firestrength = 0f;
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    HealthBarScript healthBarScript;
    [SerializeField]
    TextMeshProUGUI scoreText;

    Vector3 respawnPosition;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (healthBarScript != null)
        {
            healthBarScript.UpdateHealthDisplay(currentHealth, maxHealth);
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: 0"; // Initialize score display at start
        }
    }

    void OnInteract()
    {
        if (canInteract)
        {
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
            }
            else if (currentGem != null)
            {
                Debug.Log("Interacting with gem");
                currentGem.Collect(this);
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact();
            }
        }
    }

    public void ModifyScore(int amt)
    {
        currentScore += amt;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
        else
        {
            Debug.LogWarning("ScoreText UI is not assigned!");
        }
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (healthBarScript != null)
        {
            healthBarScript.UpdateHealthDisplay(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            // Update UI again to show 0 health immediately before respawn
            if (healthBarScript != null)
            {
                healthBarScript.UpdateHealthDisplay(currentHealth, maxHealth);
            }

            Respawn();
        }
    }

    public void SetRespawnPoint(Vector3 newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
        Debug.Log("Respawn point set to: " + respawnPosition);
    }

    void Respawn()
    {
        Debug.Log("Respawning player...");
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = respawnPosition;
            characterController.enabled = true;
        }
        else
        {
            transform.position = respawnPosition;
        }

        currentHealth = maxHealth;

        if (healthBarScript != null)
        {
            healthBarScript.UpdateHealthDisplay(currentHealth, maxHealth);
        }
    }

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * firestrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.CompareTag("Collectible"))
        {
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
        }
        else if (other.CompareTag("Collectable"))
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
            SetRespawnPoint(other.transform.position);
            other.GetComponent<RecoveryBehaviour>().RecoverHealth(this);
        }
        else if (other.CompareTag("Damage"))
        {
            Debug.Log("Taking damage");
            other.GetComponent<DamageBehaviour>().DealDamage(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (currentCoin != null && other.gameObject == currentCoin.gameObject)
        {
            canInteract = false;
            currentCoin = null;
        }
        else if (currentGem != null && other.gameObject == currentGem.gameObject)
        {
            canInteract = false;
            currentGem = null;
        }
        else if (currentDoor != null && other.gameObject == currentDoor.gameObject)
        {
            canInteract = false;
            currentDoor = null;
        }
        else if (currentRecovery != null && other.gameObject == currentRecovery.gameObject)
        {
            canInteract = false;
            currentRecovery = null;
        }
        else if (currentDamage != null && other.gameObject == currentDamage.gameObject)
        {
            canInteract = false;
            currentDamage = null;
        }
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
            else if (hitInfo.collider.CompareTag("Collectable"))
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
