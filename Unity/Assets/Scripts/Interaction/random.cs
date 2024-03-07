using UnityEngine;

public class RandomInteractable : MonoBehaviour, IInteractable
{
    // This method is called when the GameObject is interacted with
    public void Interact()
    {
        // Generate and log a random number
        int randomNumber = Random.Range(0, 100);
        Debug.Log("Random number: " + randomNumber);
    }
}
