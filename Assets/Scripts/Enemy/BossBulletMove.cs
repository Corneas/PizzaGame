using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMove : MonoBehaviour
{
    public float bulletSpeed = 20f;

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
