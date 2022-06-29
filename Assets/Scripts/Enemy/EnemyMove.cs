using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private MeshRenderer meshRenderer = null;
    private PlayerAttack playerAttack = null;
    private NavMeshAgent agent = null;

    void Start()
    {
        target = GameObject.Find("PizzaTarget");
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);

        if(agent.remainingDistance >= 1.0f)
        {

            Vector3 direction = agent.desiredVelocity;

            Quaternion rot = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10.0f);

            //agent.SetDestination(target.transform.position);
            //agent.isStopped = false;
        }
        
        if(hp <= 0)
        {
            meshRenderer.material = material;
            if (playerAttack.isTomatoSkillOn)
            {
                Bake();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Topping"))
        {
            Destroy(other.gameObject);
            if(hp > 0)
            {
                hp--;
            }
        }

        if (other.gameObject.CompareTag("Tomato"))
        {
            if (hp > 0)
            {
                hp--;
            }
        }

        if (other.gameObject.CompareTag("FireBall"))
        {
            Bake();
            Destroy(other.gameObject);
        }
    }

    public void Bake()
    {
        Destroy(gameObject);
        GameManager.Instance.curDoughCount--;
        if (hp <= 0)
        {
            pizzaObj = Instantiate(pizza, new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), Quaternion.identity);
        }
    }
}
