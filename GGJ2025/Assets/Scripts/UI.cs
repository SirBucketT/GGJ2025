using UnityEngine;

public class UI : MonoBehaviour
{
    
    [SerializeField] GameObject pauseMenu;
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    
    void Update()
    {
        //checks if escape button is pressed and reverses whatever state the pause menu UI elements & time is in
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isPaused = !pauseMenu.activeSelf;
            pauseMenu.SetActive(isPaused);
            
            Time.timeScale = isPaused ? 0 : 1;
        }    
    }
}
