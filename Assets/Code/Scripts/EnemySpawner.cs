using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{


[Header("References")]
[SerializeField] private GameObject[] enemyPrefabs;

[Header("Attributes")]
[SerializeField] private int baseEnemies = 8;
[SerializeField] private float enemiesPerSecond = 0.5f;
[SerializeField] private float timeBetweenWaves = 5f;
[SerializeField] private float difficultyScalingFactor = 0.75f; //Cuanto mas grande sea este valor mas enemigos spawnearan o mas fuertes serán los enemigos que spawnen

[Header("Events")]
public static UnityEvent onEnemyDestroy = new UnityEvent();



[SerializeField] public static int currentWave = 1;
private float timeSinceLastSpawn;
private int enemiesAlive;
private int enemiesLeftToSpawn;
private bool isSpawning = false;

    private void  Awake() 
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }
    

    private void Update()                                   //Si el juego va a 60fps, este metodo se ejecutara 60 veces por segundo.
    {
        if (!isSpawning) return;                            //Para evitar que se meta en codigo innecesariamente entre distintas oledas

        timeSinceLastSpawn += Time.deltaTime;               //Se acumula el tiempo desde el ultimo enemigo generado
                                                            //Time.deltaTime -> tiempo que ha pasado desde el ultimo fotograma
                                                            
        //la division calcula el tiempo que deberia pasar entre la generacion de dos enemigos consecutivos.
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
            
        }

        if (enemiesLeftToSpawn == 0 && enemiesAlive == 0) {
            EndWave();
        }
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private IEnumerator StartWave() //Lo hacemos IEnumerator para que nos deje llamarlo en mitad de una ejecución y hacer una pausa
    {
        if (currentWave >= 3) {
            enemiesPerSecond = 2f;
            difficultyScalingFactor = 1.3f;
        } else if (currentWave >= 5) {
            enemiesPerSecond = 3f;
        }
        yield return new WaitForSeconds(timeBetweenWaves); //Pasados x segundos empezará la primera oleada o la segunda
        isSpawning = true; //Hasta pasados los x segundos esta variable sigue en false y en el metodo update no pasa nada.
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    // private void EndWave() {
    //     isSpawning = false;
    //     timeSinceLastSpawn = 0f; //Lo ponemos en 0 para que no haya problemas con la siguiente oleada
    //     currentWave++;
    //     StartCoroutine(StartWave());
    // }

    private void EndWave() {
    isSpawning = false;
    timeSinceLastSpawn = 0f; //Lo ponemos en 0 para que no haya problemas con la siguiente oleada
    
    if (enemiesAlive == 0) {
        StartCoroutine(IncrementCurrentWave());
    }
    StartCoroutine(StartWave());
}

private IEnumerator IncrementCurrentWave() {
    yield return new WaitForSeconds(0.1f); //Esperamos un momento para que el método `EndWave()` termine
    currentWave++;
}

    private void SpawnEnemy() {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity); //Quaternion.identity -> no rotation
    }

  

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));

    }
}