using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void OnDamage(float damage,Vector3 hitpoint, Vector3 hitNormal);
    //데미지, 맞은 애, 걔 방향 이 세가지만 알면 공격 가능
}
