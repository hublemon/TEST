using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public float startinghealth = 200f;
    public float health { get; protected set; }  //�ڽ� Ŭ������ ����
    public bool dead { get; protected set; }

    protected virtual void OnEnable()  //����ü�� Ȱ��ȭ�� �� ���¸� ����(���� ���� �ٽ� �����ϸ� ���µ� ���·� �����ϰڴٴ� ��)
    {
        //virtual�̾ �ڽ��� Ȯ�尡���ѵ� �ڽĸ� ���� �����ϵ��� protected
        dead = false;
        health = startinghealth;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }


    public virtual void RestoreHealth(float newHealth)  //�޴� �ֵ����׼� Ȯ���Ű�ڴ� 
    {
        if (dead)
        {
            return;
        }

        health += newHealth;

    }
     //Start is called before the first frame update
    public virtual void Die()
    {
        dead = true;
    }
}