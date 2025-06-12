using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool isOpen = false; // Track door state

    public void Interact()
    {
        Vector3 doorRotation = transform.eulerAngles;

        if (!isOpen)
        {
            doorRotation.y += 90f; // Open the door
        }
        else
        {
            doorRotation.y -= 90f; // Close the door
        }

        transform.eulerAngles = doorRotation;
        isOpen = !isOpen; // Toggle door state
    }

    void Start()
    {

    }

    void Update()
    {

    }
}