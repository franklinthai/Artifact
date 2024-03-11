using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPosX")) {
            Debug.Log(PlayerPrefs.GetFloat("PlayerPosX"));
            float playerPosX = PlayerPrefs.GetFloat("PlayerPosX");
            float playerPosY = PlayerPrefs.GetFloat("PlayerPosY");
            float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ");
            Transform character = GameObject.FindGameObjectWithTag("Player").transform;
            character.position = new Vector3(playerPosX, playerPosY, playerPosZ);
            Debug.Log("Player pos" + character.position);
            Debug.Log("load pos ");
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
    }

    // Update is called once per frame
    void Update()
    {   
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        // SaveCharacterPosition(controller.transform.position);
    }

}
