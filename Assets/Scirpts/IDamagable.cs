using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    void OnDamage(float damage,Vector3 hitpoint, Vector3 hitNormal);
    //������, ���� ��, �� ���� �� �������� �˸� ���� ����
}
