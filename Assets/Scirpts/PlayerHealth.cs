using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    PlayerMovement playerMovement;
    Shoot playershoot;

    protected override void OnEnable()  //�״�� ��������
    {
        base.OnEnable();
        //playerMovement.enabled = true;
        //playershoot.enabled = true;  //�ٽ� �����ϸ� �Բ� ���ƿ���
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
            //Item������Ʈ �������� �õ�
            IItem item = other.GetComponent<IItem>();

            if (item != null)
            {
                item.Use(gameObject);
            }
        }
    }

}
