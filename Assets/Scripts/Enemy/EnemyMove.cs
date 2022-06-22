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

    [SerializeField]
    private Material material;

    public int hp { private set; get; } = 10;

    private GameManager gameManager = null;
    private MeshRenderer meshRenderer = null;

    void Start()
    {
        target = GameObject.Find("PizzaTarget");
        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) >= 1f)
        {
            transform.LookAt(target.transform);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }

        if(hp <= 0)
        {
            meshRenderer.material = material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Topping"))
        {
            if(hp > 0)
            {
                hp--;
            }
        }

        if (other.gameObject.CompareTag("FireBall"))
        {
            Bake();
        }
    }

    public void Bake()
    {
        Destroy(gameObject);
        gameManager.curDoughCount--;
        if (hp <= 0)
        {
            pizzaObj = Instantiate(pizza, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
        }
    }
}
