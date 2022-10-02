using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour,IItem
{
    //������ ������ ����

    // ź�� ����
    public void Use(GameObject target)
    {
        //Use�� �÷��̾��ʿ��� Use(gameObject) ��
        PlayerHealth playerhealth = GetComponent<PlayerHealth>();
        Shoot playershoot = target.GetComponent<Shoot>();
        Animator playeranimaotor = target.GetComponent<Animator>();

        //if (playershoot != null)                     //ammo shoot�� ���� health���� ontriggerenter�Ἥ ��û �ظ̳�..
        if (!playerhealth.dead)
        {
            playershoot.AmmoPresent = playershoot.AmmoMax;
            playeranimaotor.SetTrigger("Raload");

            Debug.Log("źâ�� �� �����ƴ�!");
        }
        Destroy(gameObject);

    }

}
