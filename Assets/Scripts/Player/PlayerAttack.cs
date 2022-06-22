using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPre = null;
    [SerializeField]
    private GameObject fireballPre = null;
    [SerializeField]
    private Transform bulletPos = null;
    private float bulletDelay = 0.1f;
    public float skillDelay { private set; get; } = 0;
    public float curDelay { private set; get; } = 0;

    private BulletType bulletType = null;
    private UIManager uIManager = null;
    private EnemyMove enemyMove = null;

    public bool isTomatoSkillOn = false;

    private void Awake()
    {
        bulletType = GetComponent<BulletType>();
        uIManager = FindObjectOfType<UIManager>();
        enemyMove = FindObjectOfType<EnemyMove>();
    }

    void Start()
    {
        StartCoroutine(Fire());
    }

    private void Update()
    {
        ChangeToppingType();

        if (Input.GetMouseButtonDown(1))
        {
            FireBall();
        }
        Skill();

        curDelay += Time.deltaTime;
    }

    void ChangeToppingType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletPre = bulletType.cheesePre;
            bulletType.curToppingType = ToppingType.Cheese;
            skillDelay = 5f;
            Debug.Log("토핑 변경 (치즈)");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletPre = bulletType.onionPre;
            bulletType.curToppingType = ToppingType.Onion;
            skillDelay = 0f;//30f;
            Debug.Log("토핑 변경 (양파)");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletPre = bulletType.tomatoPre;
            bulletType.curToppingType = ToppingType.Tomato;
            skillDelay = 0f;//60f;
            Debug.Log("토핑 변경 (토마토)");
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject bullet = Instantiate(bulletPre, bulletPos);
                bullet.transform.SetParent(null);
                yield return new WaitForSeconds(bulletDelay);
            }

            yield return null;
        }
    }

    void FireBall()
    {
        if(uIManager.fireballCount > 0)
        {
            uIManager.fireballCount--;
            GameObject fireball = Instantiate(fireballPre, bulletPos);
            fireball.transform.SetParent(null);
            Destroy(fireball, 3f);
        }
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bulletType.curToppingType == ToppingType.Cheese)
            {
                if(curDelay >= skillDelay)
                {
                    curDelay = 0f;
                    uIManager.skillCoolDownImage.fillAmount = 1;
                    StartCoroutine(CheeseSkill());
                }
            }
            if (bulletType.curToppingType == ToppingType.Onion)
            {
                if (curDelay >= skillDelay)
                {
                    curDelay = 0f;
                    uIManager.skillCoolDownImage.fillAmount = 1;
                    StartCoroutine(OnionSkill());
                }
            }
            if (bulletType.curToppingType == ToppingType.Tomato)
            {
                if (curDelay >= skillDelay)
                {
                    curDelay = 0f;
                    uIManager.skillCoolDownImage.fillAmount = 1;
                    StartCoroutine(TomatoSkill());
                }
            }
        }
    }
    
    IEnumerator CheeseSkill()
    {
        for(int i = -30; i < 60; i += 30)
        {
            GameObject bullet = Instantiate(bulletPre, bulletPos);
            bullet.transform.Rotate(new Vector3(0, i, 0));
            bullet.transform.SetParent(null);
            yield return new WaitForSeconds(0.05f);
        }
        yield break;
    }

    IEnumerator OnionSkill()
    {
        bulletDelay = 0.05f;
        yield return new WaitForSeconds(10f);
        bulletDelay = 0.1f;
        yield break;
    }

    IEnumerator TomatoSkill()
    {
        if(enemyMove.hp <= 0)
        {
            enemyMove.Bake();
        }
        yield return new WaitForSeconds(10f);
        yield break;
    }

}
