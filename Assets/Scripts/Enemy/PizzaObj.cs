using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaObj : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private void Start()
    {
        target = GameObject.Find("PizzaTarget");
    }

    private void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) <= 3f)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject);
                UIManager.Instance.finishedPizza++;
            }
        }
    }
}
