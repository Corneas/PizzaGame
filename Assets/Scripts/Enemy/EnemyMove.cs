using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private GameObject pizza;

    private GameObject target;


    void Start()
    {
        target = GameObject.Find("PizzaTarget");
    }

    void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) >= 1f)
        {
            transform.LookAt(target.transform);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FireBall"))
        {
            Destroy(gameObject);
            Instantiate(pizza, transform.position, Quaternion.identity);
        }
    }
}
