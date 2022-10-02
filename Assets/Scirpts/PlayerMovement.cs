using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //상하좌우 움직임+점프 구현
    //애니메이션 구현

    public float speed=3f;
    InputManager playerInput;
    Rigidbody playerRigidbody;
    Animator playerAnimator;

    RaycastHit hit;
    public float TurnSpeed = 5f;
    Quaternion dir; //회전량

    //인풋매니저도 플레이어에 할당해야함

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

        //클릭한 곳으로 방향 옮기기
        if (Input.GetMouseButton(1))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500f);
            Vector3 click = hit.point;   //클릭한 곳 저장  

            click.y = transform.position.y; //y축 회전은 하지 마라

            dir = Quaternion.LookRotation((click - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, dir, TurnSpeed * Time.deltaTime);
        }

    }

    void MoveVer()
    {
        if (playerInput.moveVer > 0)  //이거해줘야지 앞 방향을 잘 바라봄
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 0.5f);

            playerRigidbody.MovePosition(playerRigidbody.position +transform.forward * Time.deltaTime * speed);
            //캐릭터가 바라보는 방향 기준
        }
        if (playerInput.moveVer<0)
        { 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 0.5f); //Vector3는 절대좌표임

            playerRigidbody.MovePosition(playerRigidbody.position-transform.forward * Time.deltaTime * speed);
            //캐릭터가 바라보는 방향 기준
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

