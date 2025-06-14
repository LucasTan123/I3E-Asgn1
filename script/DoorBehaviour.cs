using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Tracks whether the door is open or closed
    private bool isOpen = false;

    // Called when the player interacts with the door
    public void Interact()
    {
        if (!isOpen)
        {
            // Rotate door 90 degrees to open it
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90f, transform.eulerAngles.z);
        }
        else
        {
            // Rotate door back 90 degrees to close it
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90f, transform.eulerAngles.z);
        }

        // Toggle door state
        isOpen = !isOpen;
    }
}
