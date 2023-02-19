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
    // ���� : �׳� �����ͼ� FixedUpdate��� ��� �� ��ȸ�ϸ��.
    // Rigidbody ���� ���� : ���ÿ��� AddForce�� �������༭
    public void Update()
    {
        // ���� UI ������Ʈ�� ��ġ����� �Ǿ��� ��� �� ��쿡�� ������ ó���� AnchorCreator.cs���� ����.
        if (EventSystem.current.currentSelectedGameObject)
            return;
        if (stick == null || gameObject.activeSelf == false)
            return;
        // Vector3 moveDir = (Vector3.forward * stick.Vertical) + (Vector3.right * stick.Horizontal);

        // Vector3 moveDir = new Vector3(Vector3.forward * stick.Vertical,0f, Vector3.right * stick.Horizontal);
        // �������� �ݴ�� ������ ����
        Vector3 moveDir = new Vector3(stick.Vertical, 0f, (stick.Horizontal * -1)).normalized;
        // no movement in y


        tr.Translate(moveDir * speed * Time.deltaTime, Space.World);
        if(moveDir != Vector3.zero)
            tr.rotation = Quaternion.Slerp(tr.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 5.0f);
    }









    // ���� Ÿ�ݿ� ������ �ٵ� �־����� �߰��Ұ�
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
