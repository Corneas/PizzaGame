using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    // ������ ���� ����
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE,
        PLAYERDIE
    }

    // ������ ���� ����
    public State state = State.IDLE;
    // ���� �����Ÿ�
    public float traceDist = 10.0f;
    // ���� �����Ÿ�
    public float attackDist = 2.0f;
    // ������ ��� ����
    public bool isDie = false;

    private Transform monsterTransform;
    private Transform playerTransform;
    private NavMeshAgent agent;
    private Animator anim;

    // Animator �ؽ� �� ����
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int playerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    // ���� ȿ�� ������
    private GameObject bloodEffect;

    private int initHp = 100;
    private int curHp;

    private void Awake()
    {
        monsterTransform = GetComponent<Transform>();

        playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent �ڵ� ȸ�� ��� ��Ȱ��ȭ
        agent.updateRotation = false;

        anim = GetComponent<Animator>();

        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");
    }

    void Start()
    {
        //curHp = initHp;

        //monsterTransform = GetComponent<Transform>();

        //playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //agent = GetComponent<NavMeshAgent>();

        //anim = GetComponent<Animator>();

        //bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

    }

    private void Update()
    {
        // ���������� ���� �Ÿ��� ȸ�� ���� �Ǵ�
        if(agent.remainingDistance >= 2.0f)
        {
            Vector3 direction = agent.desiredVelocity;

            Quaternion rot = Quaternion.LookRotation(direction);

            monsterTransform.rotation = Quaternion.Slerp(monsterTransform.rotation, rot, Time.deltaTime * 10.0f);
        }
    }

    private void OnEnable()
    {
        curHp = initHp;

        //monsterTransform = GetComponent<Transform>();

        //playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        //agent = GetComponent<NavMeshAgent>();

        //anim = GetComponent<Animator>();

        //bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");

        state = State.IDLE;

        curHp = initHp;
        isDie = false;

        // ���� �ݶ��̴� Ȱ��ȭ
        GetComponent<CapsuleCollider>().enabled = true;
        // ���� ��ġ �ݶ��̴� Ȱ��ȭ
        SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();
        foreach (SphereCollider sphere in spheres)
        {
            sphere.enabled = true;
        }

        // ������ ���� üũ �ڷ�ƾ
        StartCoroutine(CheckMonsterState());

        // ���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if(state == State.DIE)
            {
                yield break;
            }

            if (state == State.PLAYERDIE)
            {
                yield break;
            }

            float distance = Vector3.Distance(monsterTransform.position, playerTransform.position);



            if(distance <= attackDist)
            {
                state = State.ATTACK;
            }
            else if(distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTransform.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;
                case State.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    // ���� �ݶ��̴� ��Ȱ��ȭ
                    GetComponent<CapsuleCollider>().enabled = false;
                    // ���� ��ġ �ݶ��̴� ��Ȱ��ȭ
                    SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();
                    foreach(SphereCollider sphere in spheres)
                    {
                        sphere.enabled = false;
                    }

                    // ���� �ð� ��� �� ������Ʈ Ǯ�� ȯ��
                    yield return new WaitForSeconds(3.0f);

                    // ���� ��Ȱ��ȭ
                    this.gameObject.SetActive(false);
                    break;
                case State.PLAYERDIE:
                    StopAllCoroutines();

                    agent.isStopped = true;
                    anim.SetFloat(hashSpeed, Random.Range(0.8f, 1.3f));
                    anim.SetTrigger(playerDie);     

                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            Destroy(collision.gameObject);

            anim.SetTrigger(hashHit);

            // �浹 ����
            Vector3 pos = collision.GetContact(0).point;
            // �Ѿ� �浹 ������ ���� ����
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);

            ShowBloodEffect(pos, rot);

            // ������ hp ����
            curHp -= 10;
            if(curHp <= 0)
            {
                state = State.DIE;
                GameMgr.GetInstance().DisplayScore(50);
            }
        }
    }

    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        // ���� ȿ�� ����
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTransform);

        Destroy(blood, 1.0f);

    }

    void OnPlayerDie()
    {
        state = State.PLAYERDIE;
    }

    private void OnDrawGizmos()
    {
        if(state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(monsterTransform.position, traceDist);
        }
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(monsterTransform.position, attackDist);
        }
    }
}
