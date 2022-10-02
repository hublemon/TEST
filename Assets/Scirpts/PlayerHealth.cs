using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    PlayerMovement playerMovement;
    Shoot playershoot;

    protected override void OnEnable()  //그대로 가져가기
    {
        base.OnEnable();
        //playerMovement.enabled = true;
        //playershoot.enabled = true;  //다시 시작하면 함께 돌아오기
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();
        playerMovement.enabled = false;
        playershoot.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playershoot = GetComponent<Shoot>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            //Item컴포넌트 가져오기 시도
            IItem item = other.GetComponent<IItem>();

            if (item != null)
            {
                item.Use(gameObject);
            }
        }
    }

}
