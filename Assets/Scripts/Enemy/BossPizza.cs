using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossPizza : MonoBehaviour
{

    private int hp = 500;

    private GameManager gameManager = null;
    private UIManager uIManager = null;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uIManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {

        if(hp <= 0)
        {
            Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Topping"))
        {
            hp--;
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
        gameManager.isBossSpawn = false;
        uIManager.fireballCount += 10;
    }
}
