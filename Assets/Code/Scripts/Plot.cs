using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter() //Al ponernos encima del plot con el cursor
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()   //A dejar de apuntar con el cursor a un plot
    {
        sr.color = startColor;
    }

    private void OnMouseDown()   //A pusar un plot
    {
        if (tower != null) return;
        
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) {
            Debug.Log("Not enough credits");
            return;
            
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);  
    }
}
