using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossPizza : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countDownText;

    float countDownTimer = 120f;

    private GameManager gameManager = null;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        countDownText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (gameManager.isBossSpawn)
        {
            countDownTimer -= Time.deltaTime;
        }

        countDownText.text = countDownTimer.ToString();
    }
}
