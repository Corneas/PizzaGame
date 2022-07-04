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
    [SerializeField]
    private GameObject turret = null;
    private float bulletDelay = 0.1f;
    public float skillDelay { private set; get; } = 3f;
    public float curDelay { private set; get; } = 0;

    private BulletType bulletType = null;

    public bool isTomatoSkillOn = false;

    private void Awake()
    {
        bulletType = GetComponent<BulletType>();
    }

    void Start()
    {
        StartCoroutine(Fire());
    }

    private void Update()
    {
        ChangeToppingType();

        Skill();

        curDelay += Time.deltaTime;
    }

    void ChangeToppingType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTopping(ToppingType.Cheese);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTopping(ToppingType.Pepperoni);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTopping(ToppingType.Tomato);
        }
    }

    private void ChangeTopping(ToppingType type)
    {
        bulletPre = bulletType.toppingPre[(int)type];
        bulletType.curToppingType = type;
        skillDelay = 3f;
        Debug.Log("토핑 변경 " + type);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            // if (UIManager.Instance.isPause) continue;

            if (Input.GetMouseButton(0))
            {
                GameObject bullet = Instantiate(bulletPre, bulletPos);
                bullet.transform.SetParent(null);
                yield return new WaitForSeconds(bulletDelay);
            }

            if (Input.GetMouseButtonDown(1))
            {
                FireBall();
            }

            yield return null;
        }
    }

    void FireBall()
    {
        if(UIManager.Instance.fireballCount > 0)
        {
            UIManager.Instance.fireballCount--;
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
                    UIManager.Instance.skillCoolDownImage.fillAmount = 1;
                    StartCoroutine(CheeseSkill());
                }
            }
            if (bulletType.curToppingType == ToppingType.Pepperoni)
            {
                if (curDelay >= skillDelay)
                {
                    curDelay = 0f;
                    UIManager.Instance.skillCoolDownImage.fillAmount = 1;
                    StartCoroutine(OnionSkill());
                }
            }
            if (bulletType.curToppingType == ToppingType.Tomato)
            {
                if (curDelay >= skillDelay)
                {
                    curDelay = 0f;
                    UIManager.Instance.skillCoolDownImage.fillAmount = 1;
                    StartCoroutine(TomatoSkill());
                }
            }
        }
    }
    
    IEnumerator CheeseSkill()
    {
        for(int j = 0; j < 5; j++)
        {
            for (int i = -30; i < 60; i += 18)
            {
                GameObject bullet = Instantiate(bulletPre, bulletPos);
                bullet.transform.Rotate(new Vector3(0, i, 0));
                bullet.transform.SetParent(null);
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield break;
    }

    IEnumerator OnionSkill()
    {
        GameObject turretObj = Instantiate(turret, transform);
        yield return new WaitForSeconds(5f);
        yield break;
    }

    int random;
    float randomSpeed = 0;
    IEnumerator TomatoSkill()
    {
        for (int i = 0; i < 8; i++) 
        {
            GameObject tomato = Instantiate(bulletPre, bulletPos);
            tomato.tag = "Tomato";
            random = Random.Range(-45, 45);
            randomSpeed = Random.Range(10f, 20f);
            tomato.transform.Rotate(new Vector3(0, random, 0));
            tomato.transform.localScale = tomato.transform.localScale * 5f;
            tomato.transform.SetParent(null);
            tomato.GetComponent<BulletMove>().speed = randomSpeed;
        }
            yield break;
    }

}
