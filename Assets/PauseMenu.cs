using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject MenuPanel;
    public void Pause() {
        Time.timeScale = 0f;
        PauseButton.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void Restart() {
        EnemySpawner.currentWave = 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Current Scene

    }

    public void Quit() {
        EnemySpawner.currentWave = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //Previous Scene (Main Menu)
    }
        
    
}
