using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
    public float startinghealth = 200f;
    public float health { get; protected set; }  //자식 클래스만 접근
    public bool dead { get; protected set; }

    protected virtual void OnEnable()  //생명체가 활성화될 때 상태를 리셋(만약 게임 다시 시작하면 리셋된 상태로 시작하겠다는 것)
    {
        //virtual이어서 자식이 확장가능한데 자식만 접근 가능하도록 protected
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


    public virtual void RestoreHealth(float newHealth)  //받는 애들한테서 확장시키겠다 
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