using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour,IItem
{
    //닿으면 무조건 충전

    // 탄알 충전
    public void Use(GameObject target)
    {
        //Use를 플레이어쪽에서 Use(gameObject) 함
        PlayerHealth playerhealth = GetComponent<PlayerHealth>();
        Shoot playershoot = target.GetComponent<Shoot>();
        Animator playeranimaotor = target.GetComponent<Animator>();

        //if (playershoot != null)                     //ammo shoot에 쓰고 health에다 ontriggerenter써서 엄청 해맸네..
        if (!playerhealth.dead)
        {
            playershoot.AmmoPresent = playershoot.AmmoMax;
            playeranimaotor.SetTrigger("Raload");

            Debug.Log("탄창이 다 충전됐다!");
        }
        Destroy(gameObject);

    }

}
