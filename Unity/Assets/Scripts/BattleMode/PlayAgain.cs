using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string sceneName = "WorldScene";
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //PlayerPrefs.DeleteKey("PlayerPosX");
        //PlayerPrefs.DeleteKey("PlayerPosY");
        //PlayerPrefs.DeleteKey("PlayerPosZ");
        //PlayerPrefs.DeleteKey("level");
        //// Load the scene
        //SceneManager.LoadScene(sceneName);
    }

    public void DoSomething()
    {
        string sceneName = "WorldScene";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.DeleteKey("level");
        // Load the scene
        SceneManager.LoadScene(sceneName);
    }

    
}
