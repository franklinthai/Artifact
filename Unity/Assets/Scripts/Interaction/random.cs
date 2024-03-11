using UnityEngine;
using UnityEngine.SceneManagement;

public class RandoInteractable : MonoBehaviour, IInteractable
{
    // This method is called when the GameObject is interacted with
    public string sceneName;
    private Transform characterTransform;
    public GameObject playerObj;

    private int level;

    void Start()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (!PlayerPrefs.HasKey("level")) {
            PlayerPrefs.SetInt("level", 1);
        }
        level = PlayerPrefs.GetInt("level");
    }

    public void Interact()
    {
        // code to save position in world scene
        SaveCharacterPosition(characterTransform.position);

        sceneName = "BattleScene" + level;
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
        PlayerPrefs.SetInt("level", level + 1);
        PlayerPrefs.Save();
    }
}
