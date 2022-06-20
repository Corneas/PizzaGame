using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("PizzaDough")]
    [SerializeField]
    private GameObject pizzaDough = null;
    [SerializeField]
    private Transform[] doughSpawnPoint = null;

    private bool isGameOver = false;
    private int randomSpawnPoint = 0;

    public int curDoughCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnDough());
    }

    IEnumerator SpawnDough()
    {
        while (!isGameOver)
        {
            randomSpawnPoint = Random.Range(0, doughSpawnPoint.Length);
            Instantiate(pizzaDough, doughSpawnPoint[randomSpawnPoint]);
            curDoughCount++;
            yield return new WaitForSeconds(10f);
        }
    }
}
