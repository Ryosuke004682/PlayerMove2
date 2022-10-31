using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //CharacterController�ŎO�l�̎��_�̃v���C���[�ړ������

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
            //�� = atan2(y / x)
            //Atan2�֐����g���āA�ŏ���Y�ɍ��W��^���A����X���W��^���Ċp�x�����߂�B
            //��]�́Ax����0���甽���v���B
            //�������A�L�����N�^�[���O���������Ă�Ƃ��ɂ͉�]���[���ŁA�������玞�v���ɑ��������Ȃ���΂Ȃ�Ȃ�
            //�Ȃ̂ŁAMathf.Atan2(y , x)�ł͂Ȃ��A���̔��΂�Atan(x , y)�����Ă�����B
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y ;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle , ref _rotateVelocity , _rotateSpeed);

            transform.rotation = Quaternion.Euler(0.0f ,targetAngle , 0.0f);


            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle , 0.0f) * Vector3.forward;

            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
    }

}
