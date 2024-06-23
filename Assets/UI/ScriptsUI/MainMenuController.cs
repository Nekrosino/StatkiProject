using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenuController : MonoBehaviour
{

    VisualElement root;
    Button StartButton;
    Button QuitButton;
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        StartButton = root.Q<Button>("StartButton");
        QuitButton = root.Q<Button>("QuitButton");

        HandleStart();
        HandleQuit();

    }

    public void HandleQuit()
    {
        QuitButton.clicked += () =>
        {
            Debug.Log("Wyszedles z gry");
            Application.Quit();
        };
    }

    public void HandleStart()
    {
        StartButton.clicked += () =>
        {
            Debug.Log("Zacz¹³eœ grê");
            SceneManager.LoadScene("GameScene");
          
        };
    }
}
