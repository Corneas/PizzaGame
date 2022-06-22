using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("PizzaDough")]
    [SerializeField]
    private GameObject pizzaDough = null;
    [SerializeField]
    private GameObject pizzaBossDough = null;
    [SerializeField]
    private Transform[] doughSpawnPoint = null;
    [SerializeField]
    private Transform bossDoughSpawnPoint = null;

    private bool isGameOver = false;
    private int randomSpawnPoint = 0;

    private int totalDoughCount = 0;
    public int curDoughCount = 0;

    public bool isBossSpawn = false;

    private void Start()
    {
        StartCoroutine(SpawnBossPizza());
        StartCoroutine(SpawnDough());
    }

    IEnumerator SpawnDough()
    {
        while (!isGameOver)
        {
            randomSpawnPoint = Random.Range(0, doughSpawnPoint.Length);
            Instantiate(pizzaDough, doughSpawnPoint[randomSpawnPoint]);
            curDoughCount++;
            totalDoughCount++;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnBossPizza()
    {
        if (totalDoughCount == 20)
        {
            GameObject Boss = Instantiate(pizzaBossDough, bossDoughSpawnPoint);
            while(Boss.transform.position.y <= 20f)
            {
                Boss.transform.Translate(Vector3.up);
                yield return new WaitForSeconds(0.1f);
            }
        }

        isBossSpawn = true;
    }

    public void Dead()
    {

    }
}
