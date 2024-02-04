using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main; //Lo hacemos estatico para poder acceder a el desde todos lados.

    public Transform startPoint;
    public Transform [] path;

    public int currency;

    private void Awake() {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currency = 300;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            return true;
        } else {
            Debug.Log("No tienes suficiente dinero");
            return false;
        }
    }
}
