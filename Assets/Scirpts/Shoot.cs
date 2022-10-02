using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //���� ���� Ȯ��
    //public enum State {empty,Ready}
    //public State state { get; private set; }

    public float AttackDistance = 500f;
    public int AmmoMax = 200;
    public int AmmoPresent;

    //float lastAttackTime = 0f;  //��Ÿ���� ����
    //public float TimeBetAttack = 0.12f;   //ź�� �߻� ����

    public int DamageUp = 80;
    public int DamageDown = 50;          //������ (UpdateAttack�� �� ����)
    InputManager shootInput;
    Animator shootAnimator;
    PlayerMovement playerMovement;
    PlayerHealth playerhealth; 

    bool UpdateAtk = false;
    bool DownAtk = false;

    public virtual void OnEnable()
    {
        AmmoPresent = AmmoMax;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        shootInput = GetComponent<InputManager>();
        shootAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerhealth = GetComponent<PlayerHealth>();
        AmmoPresent = AmmoMax;
    }


    public void Attack()
    {
        //this.UpdateAtk = false;
        //�ִϸ��̼� ����
        if (shootInput.attack)
        {
            shootAnimator.SetTrigger("Attack");
            this.DownAtk = true;
            DownShot();
        }
        if (shootInput.upattack)
        {
            shootAnimator.SetTrigger("UpdateAttack");
            this.UpdateAtk = true;
            UpShot();
        }
    }

    public void DownShot()
    {
        //����ĳ��Ʈ�� �浹 Ȯ��
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;   //�ʱ�ȭ

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackDistance))
        {
            //�浹�� �������κ��� IDamagable �������� �õ�
            IDamagable target = hit.collider.GetComponent<IDamagable>();

            if (target != null)
            {
                if (this.DownAtk == true)
                {
                    //�������µ� �����ϸ� ������������
                    target.OnDamage(DamageDown, hit.point, hit.normal);
                    hitPosition = hit.point;
                    Debug.Log("�������� ������");
                }
            }
            else
            {
                Debug.Log("�������� �� ������");
            }
            this.AmmoPresent -= 1;
            Debug.Log("�Ѿ��� �پ���");
        }
    }

    void UpShot()
    {
        if (this.UpdateAtk == true)
        {
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;   //�ʱ�ȭ

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackDistance))
            { 
                //�浹�� �������κ��� IDamagable �������� �õ�
                IDamagable target = hit.collider.GetComponent<IDamagable>();

                if (target != null)
                {
                    if (this.DownAtk == true)
                    {
                        //�������µ� �����ϸ� ������������
                        target.OnDamage(DamageUp, hit.point, hit.normal);
                        hitPosition = hit.point;
                        Debug.Log("�������� �� ������");
                    }
                }
                else
                {
                    Debug.Log("�������� �� ������");
                }
                this.AmmoPresent -= 1;
                Debug.Log("�Ѿ��� �پ���");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.AmmoPresent > 0)   //�Ѿ��� �־�߸� ���� ����
        {
            Attack();  
        }
        else
        {
            AmmoPresent = 0;
        }
    }

}
