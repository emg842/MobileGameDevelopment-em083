using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hitPoints = 3; //Puntos de vida
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
{
    hitPoints -= dmg;

    if (hitPoints <= 0 && !isDestroyed)
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        LevelManager.main.IncreaseCurrency(currencyWorth);
        isDestroyed = true;
        Destroy(gameObject);
    }
}


    public void SetHitPoints(int newHitPoints)
{
    hitPoints = newHitPoints;
}

        

}