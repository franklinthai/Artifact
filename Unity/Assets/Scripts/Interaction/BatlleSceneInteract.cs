using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomInteractable : MonoBehaviour, IInteractable
{
    // This method is called when the GameObject is interacted with
    public string sceneName;
    public void Interact()
    {
        sceneName = "BattleScene1";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
}
