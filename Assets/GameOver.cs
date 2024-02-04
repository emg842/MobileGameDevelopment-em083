using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    [SerializeField] public GameObject GameOverPanel;
    
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
