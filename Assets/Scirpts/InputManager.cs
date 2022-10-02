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
        //이후 싱클톤을 위해서라도 게임매지저 할당-게임 오버시 변수 초기화

        moveVer = Input.GetAxis("Vertical"); //GetAxis(값을 -1~1로 받음)
        moveHor = Input.GetAxis("Horizontal");
        attack = Input.GetMouseButton(0);   //마우스 왼쪽 버튼 클릭
        upattack = Input.GetKey(KeyCode.F);
        reload = Input.GetButton("Reload");
    }
}
