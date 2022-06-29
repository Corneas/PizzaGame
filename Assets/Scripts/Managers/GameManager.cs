using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
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

    public PoolManager poolManager = null;

    private void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
        StartCoroutine(SpawnDough());
    }

    private void Update()
    {
        SpawnBossPizza();
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

    void SpawnBossPizza()
    {
        if(totalDoughCount % 10 == 0 && !isBossSpawn && totalDoughCount != 0)
        {
            GameObject Boss = Instantiate(pizzaBossDough, bossDoughSpawnPoint);
            //while(Boss.transform.position.y <= 20f)
            //{
            //    Boss.transform.Translate(Vector3.up);
            //    yield return new WaitForSeconds(0.1f);
            //}

            isBossSpawn = true;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isBossSpawn = false;
        MouseManager.Lock(false);
        MouseManager.Show(true);
        Debug.Log("게임오버");
    }
}
