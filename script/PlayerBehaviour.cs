using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth = 100;
    int currentScore = 0;
    int collectiblesCollected = 0;
    [SerializeField] int totalCollectibles = 10;

    bool canInteract = false;
    GemBehaviour currentGem = null;
    CoinBehaviour currentCoin = null;
    RecoveryBehaviour currentRecovery;
    DamageBehaviour currentDamage;
    DoorBehaviour currentDoor = null;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float firestrength = 0f;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] HealthBarScript healthBarScript;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI collectibleText;

    Vector3 respawnPoint;

    void Start()
    {
        respawnPoint = transform.position;
        UpdateHealthDisplay();
        UpdateScoreDisplay();
        UpdateCollectibleDisplay();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Respawn();
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Collectible"))
            {
                canInteract = true;
                currentCoin = hitInfo.collider.GetComponent<CoinBehaviour>();
            }
            else if (hitInfo.collider.CompareTag("Collectable"))
            {
                canInteract = true;
                currentGem = hitInfo.collider.GetComponent<GemBehaviour>();
            }
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

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * firestrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }

    void OnTriggerEnter(Collider other)
    {
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
            other.GetComponent<RecoveryBehaviour>().RecoverHealth(this);
            respawnPoint = other.transform.position; // set as new respawn point
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

    public void ModifyScore(int amt)
    {
        currentScore += amt;
        UpdateScoreDisplay();
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        else if (currentHealth <= 0)
            currentHealth = 0;

        UpdateHealthDisplay();
    }

    void Respawn()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        if (healthBarScript != null)
            healthBarScript.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore + "/300";
    }

    void UpdateCollectibleDisplay()
    {
        if (collectibleText != null)
            collectibleText.text = "Collectibles: " + collectiblesCollected + "/" + totalCollectibles;
    }

    public void CollectCollectible()
    {
        collectiblesCollected++;
        UpdateCollectibleDisplay();
    }

    public int GetScore()
    {
        return currentScore;
    }
}
