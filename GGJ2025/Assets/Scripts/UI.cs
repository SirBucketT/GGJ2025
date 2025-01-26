using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UI : MonoBehaviour
{
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameSettingsUI;
    [SerializeField] GameObject gameOverUI;
    
    void Start()
    {
        Time.timeScale = 1;
        gameSettingsUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverUI.SetActive(false);
        gameUI.SetActive(true);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager();    
        }
        
        //testing implementation of game over UI
        //if (Input.GetKeyDown(KeyCode.R))
        //{
          //  bool isGameOver = !gameOverUI.activeSelf;
            //gameOverUI.SetActive(isGameOver);
        //}
    }

    public void GameSettings()
    {
        pauseMenu.SetActive(false);
        gameSettingsUI.SetActive(true);
    }
    
    public void ReturnPauseMenu()
    {
        pauseMenu.SetActive(true);
        gameSettingsUI.SetActive(false);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameRestarter()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ResumeGame()
    {
        PauseManager();
    }
    
    //checks if the time.timescale is active or not and sets the pause menu UI elements to whatever state it's in.
    void PauseManager()
    {
        bool isPaused = !pauseMenu.activeSelf;
        pauseMenu.SetActive(isPaused);
            
        Time.timeScale = isPaused ? 0 : 1;
    }
    
    public void QuitGameButton()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit button pressed");
    #endif
        Application.Quit();
    }
}
