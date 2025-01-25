using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject GameUI;
    
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        GameUI.SetActive(true);
    }

    
    void Update()
    {
        PauseManager();    
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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
}
