using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //�� �ڲ� AI�� UI�� ����?

//����޽��� �����̳�..

public class EnemyAI : LivingEntity  //LivingEntity���� �̹� onDamage ����
{
    public LayerMask whatIsTarget;   //������� ���̾�

    LivingEntity targetEntity;   //�������
    NavMeshAgent enemeyNav;       //��� ��� ������Ʈ


    Animator enemyAnimator;

    public float damage = 40f;
    public float attackDelay = 1f; //���ݵ�����
    public float lastAttackTime;   //������ ���� ����
    public float distance;         //��������� �Ÿ�

    public float attackRange = 1.5f;


    //���� ����� �����ϴ��� Ȯ��
    bool hasTarget
    {
        get
        {
            if(targetEntity!=null&&!targetEntity.dead)
            {
                return true;    //������ ����� ������ hasTarget�� true��!
            }
            //�׷��� ������ false
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
        //���� ������Ʈ Ȱ��ȭ�� ���ÿ� AI�� ���� ��ƾ ����
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //if (hasTarget)
        //{
        //distance = Vector3.Distance(transform.position, targetEntity.transform.position);
        //������ �Ÿ�
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
                Debug.Log("���� �÷��̾ �߰��ߴ�!");
                Attack();
            }
            else
            {
                //��������� ������ AI �̵� ����
                enemeyNav.isStopped = true;
                enemyAnimator.SetBool("HasTarget", hasTarget);

                //������ 20f�� �ݶ��̴��� Target���̾ ���� �ݶ��̴� ����
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                for (int i=0;i<colliders.Length;i++)
                {
                    //�ݶ��̴��κ��� LivingEntity ������Ʈ ��������
                    LivingEntity playerEntity = colliders[i].GetComponent<LivingEntity>();
                    if(playerEntity!=null&&!playerEntity.dead)
                    {
                        this.targetEntity = playerEntity;   //��� ��꿡�� targetEntity ����

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);    //0.25�ʸ��� ��� ����
        }
    }

    //���� ��� ���� �Ÿ��� ���� ���� ����
    public virtual void Attack()   
    {
        //������ �Ÿ��� ���� ��Ÿ� �ȿ� �ִٸ�
        if (!dead && distance <= attackRange)
        {
            transform.LookAt(targetEntity.transform);
            if (lastAttackTime + attackDelay <= Time.time)   //��Ÿ�ӿ� �ɸ���
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
        else  //���� ��Ÿ� �ۿ� �ִٸ� ��� ����
        {
            enemeyNav.isStopped = false;
            enemeyNav.SetDestination(targetEntity.transform.position);
        }
    }


    //�ǰ�(�����)
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)  //shoot���� �ǰݱ�� ����
    {
        if (!dead)
        {
            enemyAnimator.SetTrigger("BeingAttack");
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }
    public override void Die() //�����
    {
        base.Die();

        //�ٸ� AI�� �������� �ʵ��� �ڽ��� ��� �ݶ��̴� ��Ȱ��ȭ //�̰� Ÿ�ٷ��̾��ũ �Ƚ����� ���͵鳢�� �ο��ڴµ�������
        Collider[] enemyColliders = GetComponents<Collider>();  //�ݶ��̴��� �ݶ��̴� ���� �� ���Ե�
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }
        //AI ���� �����ϰ� ����޽� ��Ȱ��ȭ
        enemeyNav.isStopped = true;
        enemeyNav.enabled = false;

        enemyAnimator.SetTrigger("Die");

    }
    
}
