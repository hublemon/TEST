using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //왜 자꾸 AI를 UI라 하지?

//내비메쉬가 말썽이네..

public class EnemyAI : LivingEntity  //LivingEntity에서 이미 onDamage 받음
{
    public LayerMask whatIsTarget;   //추적대상 레이어

    LivingEntity targetEntity;   //추적대상
    NavMeshAgent enemeyNav;       //경로 계산 에이전트


    Animator enemyAnimator;

    public float damage = 40f;
    public float attackDelay = 1f; //공격딜레이
    public float lastAttackTime;   //마지막 공격 시점
    public float distance;         //추적대상간의 거리

    public float attackRange = 1.5f;


    //추적 대상이 존재하는지 확인
    bool hasTarget
    {
        get
        {
            if(targetEntity!=null&&!targetEntity.dead)
            {
                return true;    //추적할 대상이 있으면 hasTarget을 true로!
            }
            //그렇지 않으면 false
            return false;
        }
    }


    private void Awake()
    {
        enemeyNav = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    public void SetUp(EnemyData enemyData)
    {
        startinghealth = enemyData.health;
        damage = enemyData.damage;
        enemeyNav.speed = enemyData.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        //게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //if (hasTarget)
        //{
        //distance = Vector3.Distance(transform.position, targetEntity.transform.position);
        //대상과의 거리
        //}
    }

    IEnumerator UpdatePath()
    {
        if (!dead)
        {
            if (hasTarget==true)
            {
                enemeyNav.isStopped = false;
                enemeyNav.SetDestination(targetEntity.transform.position);
                enemyAnimator.SetBool("HasTarget", hasTarget);
                distance = Vector3.Distance(transform.position, targetEntity.transform.position);
                Debug.Log("적이 플레이어를 발견했다!");
                Attack();
            }
            else
            {
                //추적대상이 없으면 AI 이동 멈춤
                enemeyNav.isStopped = true;
                enemyAnimator.SetBool("HasTarget", hasTarget);

                //반지름 20f의 콜라이더로 Target레이어를 가진 콜라이더 검출
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                for (int i=0;i<colliders.Length;i++)
                {
                    //콜라이더로부터 LivingEntity 컴포넌트 가져오기
                    LivingEntity playerEntity = colliders[i].GetComponent<LivingEntity>();
                    if(playerEntity!=null&&!playerEntity.dead)
                    {
                        this.targetEntity = playerEntity;   //경로 계산에서 targetEntity 지정

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);    //0.25초마다 경로 갱신
        }
    }

    //추적 대상 간의 거리에 따라 공격 실행
    public virtual void Attack()   
    {
        //대상과의 거리가 공격 사거리 안에 있다면
        if (!dead && distance <= attackRange)
        {
            transform.LookAt(targetEntity.transform);
            if (lastAttackTime + attackDelay <= Time.time)   //쿨타임에 걸리면
            {
                enemyAnimator.SetBool("Punch",true);
                lastAttackTime = Time.time;
                targetEntity.OnDamage(damage, targetEntity.transform.position,(targetEntity.transform.position-transform.position).normalized);
            }
            else
            {
                enemyAnimator.SetBool("Punch", false);
            }
        }
        else  //공격 사거리 밖에 있다면 계속 추적
        {
            enemeyNav.isStopped = false;
            enemeyNav.SetDestination(targetEntity.transform.position);
        }
    }


    //피격(기능함)
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)  //shoot에서 피격기능 구현
    {
        if (!dead)
        {
            enemyAnimator.SetTrigger("BeingAttack");
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die() //기능함
    {
        base.Die();

        //다른 AI를 방해하지 않도록 자신의 모든 콜라이더 비활성화 //이거 타겟레이어마스크 안썼으면 몬스터들끼리 싸웠겠는데ㅋㅋㅋ
        Collider[] enemyColliders = GetComponents<Collider>();  //콜라이더에 콜라이더 종류 다 포함됨
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }
        //AI 추적 중지하고 내비메쉬 비활성화
        enemeyNav.isStopped = true;
        enemeyNav.enabled = false;

        enemyAnimator.SetTrigger("Die");

    }
    
}
