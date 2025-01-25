using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UI : MonoBehaviour
{
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameSettingsUI;
    
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager();    
        }
    }

    public void GameSettings()
    {
        
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
