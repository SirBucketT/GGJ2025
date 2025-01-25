using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class MenuScripts : MonoBehaviour
{
    
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject howToPlay;

    private void Start()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(true);
        credits.SetActive(false);
        howToPlay.SetActive(false);
    }
    
    //manages states of active objects for credits and menu assets
    public void CreditsButton()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void PlayInstruction()
    {
        mainMenu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void MenuButton()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
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
