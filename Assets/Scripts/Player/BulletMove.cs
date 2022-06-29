using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    public float speed = 20f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            //ToppingDespawn();
        }
    }

    //private void ToppingDespawn()
    //{
    //    transform.SetParent(PoolManager.Instance.playerTopping.transform, false);
    //    gameObject.SetActive(false);
    //}
}
