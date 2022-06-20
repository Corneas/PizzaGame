using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    // 몬스터의 상태 정보
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE,
        PLAYERDIE
    }

    // 몬스터의 현재 상태
    public State state = State.IDLE;
    // 추적 사정거리
    public float traceDist = 10.0f;
    // 공격 사정거리
    public float attackDist = 2.0f;
    // 몬스터의 사망 여부
    public bool isDie = false;

    private Transform monsterTransform;
    private Transform playerTransform;
    private NavMeshAgent agent;
    private Animator anim;

    // Animator 해쉬 값 추출
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int playerDie = Animator.StringToHash("PlayerDie");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");

    // 혈흔 효과 프리팹
    private GameObject bloodEffect;

    private int initHp = 100;
    private int curHp;

    private void Awake()
    {
        monsterTransform = GetComponent<Transform>();

        playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();

        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent 자동 회전 기능 비활성화
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
        // 목적지까지 남은 거리로 회전 여부 판단
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

        // 몬스터 콜라이더 활성화
        GetComponent<CapsuleCollider>().enabled = true;
        // 몬스터 펀치 콜라이더 활성화
        SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();
        foreach (SphereCollider sphere in spheres)
        {
            sphere.enabled = true;
        }

        // 몬스터의 상태 체크 코루틴
        StartCoroutine(CheckMonsterState());

        // 상태에 따라 몬스터의 행동을 수행하는 코루틴
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
                    // 몬스터 콜라이더 비활성화
                    GetComponent<CapsuleCollider>().enabled = false;
                    // 몬스터 펀치 콜라이더 비활성화
                    SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();
                    foreach(SphereCollider sphere in spheres)
                    {
                        sphere.enabled = false;
                    }

                    // 일정 시간 대기 후 오브젝트 풀링 환원
                    yield return new WaitForSeconds(3.0f);

                    // 몬스터 비활성화
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

            // 충돌 지점
            Vector3 pos = collision.GetContact(0).point;
            // 총알 충돌 지점의 법선 벡터
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);

            ShowBloodEffect(pos, rot);

            // 몬스터의 hp 차감
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
        // 혈흔 효과 생성
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
