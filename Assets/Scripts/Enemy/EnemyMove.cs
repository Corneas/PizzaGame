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
    private GameObject pizzaObj;

    private int cheeseTopping = 0;
    private int tomatoTopping = 0;
    private int onionTopping = 0;

    private GameManager gameManager = null;

    void Start()
    {
        target = GameObject.Find("PizzaTarget");
        gameManager = FindObjectOfType<GameManager>();
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
        if (other.gameObject.CompareTag("Cheese"))
        {
            cheeseTopping++;
        }
        if (other.gameObject.CompareTag("Tomato"))
        {
            tomatoTopping++;
        }
        if (other.gameObject.CompareTag("Onion"))
        {
            onionTopping++;
        }

        if (other.gameObject.CompareTag("FireBall"))
        {
            Destroy(gameObject);
            gameManager.curDoughCount--;
            if(cheeseTopping >= 1 && tomatoTopping >= 1 && onionTopping >= 1)
            {
                pizzaObj = Instantiate(pizza, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
            }
        }
    }
}
