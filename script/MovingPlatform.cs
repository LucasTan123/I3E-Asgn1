using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Point A and Point B to move between
    public Transform pointA;
    public Transform pointB;

    // Speed of the platform's movement
    public float speed = 1f;

    // The current target the platform is moving toward
    private Transform target;

    void Start()
    {
        // Set the initial target to point B
        target = pointB;
    }

    void Update()
    {
        // Move the platform toward the target point at the given speed
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If the platform is very close to the target...
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            // ...switch the target to the other point
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
