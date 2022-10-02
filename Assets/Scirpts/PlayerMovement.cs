using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //�����¿� ������+���� ����
    //�ִϸ��̼� ����

    public float speed=3f;
    InputManager playerInput;
    Rigidbody playerRigidbody;
    Animator playerAnimator;

    RaycastHit hit;
    public float TurnSpeed = 5f;
    Quaternion dir; //ȸ����

    //��ǲ�Ŵ����� �÷��̾ �Ҵ��ؾ���

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MoveVer();
        MoveHor();

        //Ŭ���� ������ ���� �ű��
        if (Input.GetMouseButton(1))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500f);
            Vector3 click = hit.point;   //Ŭ���� �� ����  

            click.y = transform.position.y; //y�� ȸ���� ���� ����

            dir = Quaternion.LookRotation((click - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, dir, TurnSpeed * Time.deltaTime);
        }

    }

    void MoveVer()
    {
        if (playerInput.moveVer > 0)  //�̰�������� �� ������ �� �ٶ�
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 0.5f);

            playerRigidbody.MovePosition(playerRigidbody.position +transform.forward * Time.deltaTime * speed);
            //ĳ���Ͱ� �ٶ󺸴� ���� ����
        }
        if (playerInput.moveVer<0)
        { 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 0.5f); //Vector3�� ������ǥ��

            playerRigidbody.MovePosition(playerRigidbody.position-transform.forward * Time.deltaTime * speed);
            //ĳ���Ͱ� �ٶ󺸴� ���� ����
        }
        playerAnimator.SetFloat("Vertical", Mathf.Abs(playerInput.moveVer));
    }

    

    void MoveHor()
    {
        Vector3 Horizon = playerInput.moveHor * speed * transform.right * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + Horizon);
        playerAnimator.SetFloat("Horizon", playerInput.moveHor);
    }

  
}

