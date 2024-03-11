using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomInteractable : MonoBehaviour, IInteractable
{
    // This method is called when the GameObject is interacted with
    public string sceneName;
    private Transform characterTransform;
    void Start()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Interact()
    {
        // code to save position in world scene
        SaveCharacterPosition(characterTransform.position);

        sceneName = "BattleScene";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    public void SaveCharacterPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", position.z);
        Debug.Log("save position");
        Debug.Log(PlayerPrefs.GetFloat("PlayerPosX"));
        PlayerPrefs.Save();
    }
}
