using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuScripts : MonoBehaviour
{
    
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Credits;

    private void Start()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
    }
    
    //manages states of active objects for credits and menu assets
    public void creditsButton()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void MenuButton()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
    }
    
    //loads game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    //quits game
    public void QuitGameButton()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit button pressed");
    #endif
        Application.Quit();
    }
}
