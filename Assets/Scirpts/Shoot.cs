using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //장전 상태 확인
    //public enum State {empty,Ready}
    //public State state { get; private set; }

    public float AttackDistance = 500f;
    public int AmmoMax = 200;
    public int AmmoPresent;

    //float lastAttackTime = 0f;  //쿨타임을 위해
    //public float TimeBetAttack = 0.12f;   //탄알 발사 간격

    public int DamageUp = 80;
    public int DamageDown = 50;          //데미지 (UpdateAttack시 값 변경)
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
        //애니메이션 실행
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
        //레이캐스트로 충돌 확인
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;   //초기화

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackDistance))
        {
            //충돌한 상대방으로부터 IDamagable 가져오기 시도
            IDamagable target = hit.collider.GetComponent<IDamagable>();

            if (target != null)
            {
                if (this.DownAtk == true)
                {
                    //가져오는데 성공하면 데미지입히기
                    target.OnDamage(DamageDown, hit.point, hit.normal);
                    hitPosition = hit.point;
                    Debug.Log("데미지를 입혔다");
                }
            }
            else
            {
                Debug.Log("데미지를 못 입혔다");
            }
            this.AmmoPresent -= 1;
            Debug.Log("총알이 줄었다");
        }
    }

    void UpShot()
    {
        if (this.UpdateAtk == true)
        {
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;   //초기화

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, AttackDistance))
            { 
                //충돌한 상대방으로부터 IDamagable 가져오기 시도
                IDamagable target = hit.collider.GetComponent<IDamagable>();

                if (target != null)
                {
                    if (this.DownAtk == true)
                    {
                        //가져오는데 성공하면 데미지입히기
                        target.OnDamage(DamageUp, hit.point, hit.normal);
                        hitPosition = hit.point;
                        Debug.Log("데미지를 더 입혔다");
                    }
                }
                else
                {
                    Debug.Log("데미지를 못 입혔다");
                }
                this.AmmoPresent -= 1;
                Debug.Log("총알이 줄었다");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.AmmoPresent > 0)   //총알이 있어야만 공격 가능
        {
            Attack();  
        }
        else
        {
            AmmoPresent = 0;
        }
    }

}
