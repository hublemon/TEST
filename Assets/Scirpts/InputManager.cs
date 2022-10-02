using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Start is called before the first frame update
    
    public float moveVer  {get; private set;}
    public float moveHor { get; private set; }
    public bool attack { get; private set; }
    public bool reload { get; private set; }

    public bool upattack { get; private set; }

    // Update is called once per frame
    private void Update()
    {
        //���� ��Ŭ���� ���ؼ��� ���Ӹ����� �Ҵ�-���� ������ ���� �ʱ�ȭ

        moveVer = Input.GetAxis("Vertical"); //GetAxis(���� -1~1�� ����)
        moveHor = Input.GetAxis("Horizontal");
        attack = Input.GetMouseButton(0);   //���콺 ���� ��ư Ŭ��
        upattack = Input.GetKey(KeyCode.F);
        reload = Input.GetButton("Reload");
    }
}
