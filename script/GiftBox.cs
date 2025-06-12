using UnityEngine;

public class GiftBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject Coin;
    void OnCollisionEnter(Collision collision) // second blue collision is the collsion box of object colliding with the gift box //
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Instantiate a coin at the position of the gift box
            Instantiate(Coin, transform.position, transform.rotation);
            // Destroy the gift box after instantiating the coin
            Destroy(gameObject); // gameObject refers to the current object that has gift box code  attacjed to it//
            Destroy(collision.gameObject); // Destroy the projectile that collided with the gift box (collision = collision box of object AND gameObject is the object linked to the collision box)//
        } 
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
