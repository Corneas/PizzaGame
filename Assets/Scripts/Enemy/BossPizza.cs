using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossPizza : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPre;
    [SerializeField]
    private Transform bulletPos;
    [SerializeField]
    private GameObject target;

    private int hp = 500;

    [SerializeField]
    private Animator animator = null;

    private GameManager gameManager = null;
    private UIManager uIManager = null;

    private void Awake()
    {
        target = GameObject.Find("PizzaTarget");
        gameManager = FindObjectOfType<GameManager>();
        uIManager = FindObjectOfType<UIManager>();
        animator.SetTrigger("SpawnBoss");
        StartCoroutine(BossSkill());
    }

    private void Update()
    {
        if(hp <= 0)
        {
            animator.SetTrigger("BossDie");
            Invoke("Dead",3f);
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
        uIManager.countDownTimer = 120f;
        uIManager.fireballCount += 10;
    }

    int pattern = 0;
    IEnumerator BossSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            if (pattern == 0)
            {
                List<BossBulletMove> bossBulletMoves = new List<BossBulletMove>();
                for (int i = 0; i <= 180; i += 6)
                {
                    var bullet = Instantiate(bulletPre, bulletPos.position, Quaternion.Euler(-i, 90, 0));
                    bossBulletMoves.Add(bullet.GetComponent<BossBulletMove>());
                }
                StartCoroutine(Shot_goto(bossBulletMoves.ToArray()));
            }
        }
    }

    IEnumerator Shot_goto(BossBulletMove[] bossBulletMoves)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < bossBulletMoves.Length; i++)
        {
            bossBulletMoves[i].bulletSpeed = 0f;
        }

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < bossBulletMoves.Length; i++)
        {
            bossBulletMoves[i].bulletSpeed = 50f;
        }

        foreach (var bulletItem in bossBulletMoves)
        {
            bulletItem.transform.LookAt(target.transform);
        }
        yield return null;
    }
}
