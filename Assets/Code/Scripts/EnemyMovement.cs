using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; //Esto lo vamos a poner en el inspector pero nos va a permitir que el enemigo se mueva.

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0.5f;

    private Transform target;
    private int pathIndex = 0;

    
    void Start()
    {
        target = LevelManager.main.path[pathIndex];      // Establecemos el primer punto del camino como el objetivo inicial del enemigo.
        
    }

    //se verifica si el enemigo ha llegado al punto objetivo actual y, en ese caso, actualiza el objetivo al siguiente punto del camino. Si alcanza el último punto, se destruye.
    void Update() 
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                GameOver gameOver = FindObjectOfType<GameOver>(); 
                if (gameOver != null)
                {
                    gameOver.GameOverPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
                
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }
    private void FixedUpdate() {                                              //El enemigo se movera al siguiente elemento.
                                                                              //No depende de los fps
       Vector2 direction = (target.position - transform.position).normalized; //La direccion que tiene que tomar el enemigo para llegar al target
                                                                              //Lo de normalized es para asegurarnos que la direccion es entre 0 y 1 (y no sea mas grande que eso)
        rb.velocity = direction * moveSpeed;                                  // Asigna la velocidad al componente Rigidbody2D para mover al enemigo en la dirección calculada.                                
    }                                                                         
}
