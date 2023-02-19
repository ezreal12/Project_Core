using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    public float speed;
    public Joystick stick;
    private Transform tr;

    public void Start()
    {
        tr = GetComponent<Transform>();

    }
    // 사용법 : 그냥 가져와서 FixedUpdate등에서 계속 값 조회하면됨.
    // Rigidbody 쓰는 이유 : 예시에서 AddForce로 움직여줘서
    public void Update()
    {
        // 만일 UI 오브젝트가 터치대상이 되었을 경우 이 경우에도 별도의 처리를 AnchorCreator.cs에선 안함.
        if (EventSystem.current.currentSelectedGameObject)
            return;
        if (stick == null || gameObject.activeSelf == false)
            return;
        // Vector3 moveDir = (Vector3.forward * stick.Vertical) + (Vector3.right * stick.Horizontal);

        // Vector3 moveDir = new Vector3(Vector3.forward * stick.Vertical,0f, Vector3.right * stick.Horizontal);
        // 세로축이 반대로 보여서 반전
        Vector3 moveDir = new Vector3(stick.Vertical, 0f, (stick.Horizontal * -1)).normalized;
        // no movement in y


        tr.Translate(moveDir * speed * Time.deltaTime, Space.World);
        if(moveDir != Vector3.zero)
            tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 5.0f);
    }









    // 추후 타격용 리지드 바디 넣었을때 추가할것
    public void FixedUpdate()
    {
        //  rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        //  move forward on local x when moving joystick up or down

        /*        float forwardMove = stick.Vertical;
                if (forwardMove >= 0)
                {  // don't allow backwards movement
                    rb.AddForce(transform.right * forwardMove * speed);
                }
                // rotate on z axis when moving joystick left or right.
                rb.rotation = rb.rotation * Quaternion.AngleAxis(stick.Horizontal * speed, Vector3.forward);*/
    }
}
