using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundNumber : MonoBehaviour
{

[Header("References")]
[SerializeField] TextMeshProUGUI roundUI;

private void OnGUI()
{
     roundUI.text = EnemySpawner.currentWave.ToString();
}

  
}