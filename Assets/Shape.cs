using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour
{
    //��`
    public enum Type { circle, square, triangle}    //�`�̎��
    private enum State { run, holded, fall, stop}         //�}�`�̏��


    //�Q��
    private Camera m_cam; //�J����
    private Rigidbody2D rb; //Rigidbody

    //�ϐ�
    private bool initialized = false;   //�������t���O
    [SerializeField] private Type type;  //�`�̎��
    private State state;    //���
    [SerializeField] private float runSpeed = 5f;   //����鑬��

    //�v���p�e�B
    public Type ShapeType { get { return type; } }  //�}�`�̎��


    //������
    /// <summary>
    /// ���g�̏���������
    /// </summary>
    public void Initialize()
    {
        if(!initialized)    //����������Ă��Ȃ��ꍇ
        {
            initialized = true; //�������t���O��܂�

            //���������s��
            //�Q��
            m_cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();  //�J�������擾
            rb = GetComponent<Rigidbody2D>();   //Rigidbody
            //������
            tag = "Shape";  //�^�O�ݒ�
            ChangeState(State.run); //��Ԑݒ�
        }
    }

    //�}�`���̂̏�Ԃɂ������֐�
    /// <summary>
    /// ��Ԃ�ς��鏈��
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(State newState)
    {
        //�ݒ肷��l
        RigidbodyType2D rb_bodyType;    //Rigidbody�̃��[�h

        //������Ԃ̏ꍇ
        if(newState == State.run)
        {
            rb_bodyType = RigidbodyType2D.Kinematic;    //Rigidbody�̓���
        }
        //��������Ԃ̏ꍇ
        else if(newState == State.holded)
        {
            rb_bodyType = RigidbodyType2D.Kinematic;    //Rigidbody�̓���
        }
        //������Ԃ̏ꍇ
        else if(newState == State.fall)
        {
            rb_bodyType = RigidbodyType2D.Dynamic;  //Rigidbody�̓���
        }
        //�񊈓���Ԃ̏ꍇ
        else
        {
            rb_bodyType = RigidbodyType2D.Kinematic;    //Rigidbody�̓���
        }

        //�l��ݒ�
        state = newState;   //�}�`�̏��
        rb.bodyType = rb_bodyType;  //Rigidbody�̃��[�h
    }
    /// <summary>
    /// �񊈓���Ԃɂ��鏈��
    /// </summary>
    public void ToNotActive()
    {
        ChangeState(State.stop);    //stop��Ԃɂ���
    }
    /// <summary>
    /// ���g��������Ԃɖ߂�
    /// </summary>
    public void ResetSelf()
    {
        ChangeState(State.run); //������ԂɂȂ�
    }

    //�N���b�N�֘A�̊֐�
    /// <summary>
    /// �N���b�N���̏���
    /// </summary>
    public void OnClieckDowned()
    {
        if(state == State.run || state == State.fall)   //�����܂��͗�����Ԃ̏ꍇ
        {
            //��������Ԃɂ���
            ChangeState(State.holded);  //��ԕύX
        }
    }
    /// <summary>
    /// �h���b�O���ꂽ�یĂ΂��
    /// </summary>
    public void OnDragged()
    {
        //�h���b�O���Ă���ԁA�J�[�\���ɕt���Ĉړ�
        Vector3 mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition); //�}�E�X�ʒu�����[���h���W�Ŏ擾
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z); //�ʒu�ݒ�
    }
    /// <summary>
    /// �h���b�O�̏I�����̏���
    /// </summary>
    /// <returns></returns>
    public void OnDropped()
    {
        //������ԂɕύX
        ChangeState(State.fall);    
    }


    //update
    private void Update()
    {
        //����鏈��
        if(state == State.run)  
        {
            //���ɗ���鏈��
            transform.position += new Vector3(-runSpeed * Time.deltaTime, 0f, 0f);   //�ړ�
        }
    }
}
