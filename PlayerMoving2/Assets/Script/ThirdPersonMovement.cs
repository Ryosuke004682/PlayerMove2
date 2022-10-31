using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //CharacterControllerで三人称視点のプレイヤー移動を作る

    public CharacterController _controller;
    public Transform _cameraTransform;


    public float _speed = 6.0f;
    public float _rotateSpeed = 0.1f;
           float _rotateVelocity; 


    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal , 0.0f , vertical).normalized;



        if (direction.magnitude >= 0.1f)
        {
            //θ = atan2(y / x)
            //Atan2関数を使って、最初にYに座標を与え、次にX座標を与えて角度を求める。
            //回転は、x軸の0から反時計回り。
            //しかし、キャラクターが前方を向いてるときには回転がゼロで、そこから時計回りに増加させなければならない
            //なので、Mathf.Atan2(y , x)ではなく、その反対のAtan(x , y)を入れてあげる。
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y ;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle , ref _rotateVelocity , _rotateSpeed);

            transform.rotation = Quaternion.Euler(0.0f ,targetAngle , 0.0f);


            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle , 0.0f) * Vector3.forward;

            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
    }

}
