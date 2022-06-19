using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private GameObject target;

    void Start()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }
}
