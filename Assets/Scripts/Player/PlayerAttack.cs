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

    private BulletType bulletType = null;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            FireBall();
        }
    }

    void ChangeToppingType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletPre = bulletType.cheesePre;
            bulletType.curToppingType = ToppingType.Cheese;
            Debug.Log("토핑 변경 (치즈)");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletPre = bulletType.onionPre;
            bulletType.curToppingType = ToppingType.Onion;
            Debug.Log("토핑 변경 (양파)");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletPre = bulletType.tomatoPre;
            bulletType.curToppingType = ToppingType.Tomato;
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
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }

    void FireBall()
    {
        GameObject fireball = Instantiate(fireballPre, bulletPos);
        fireball.transform.SetParent(null);
        Destroy(fireball, 3f);
    }

}
