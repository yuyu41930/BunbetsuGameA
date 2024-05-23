using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBox_ShapeSenser : MonoBehaviour
{
    //�Q��
    private ShapeBox box;   //�}�`�̔�


    //start
    private void Start()
    {
        box = transform.parent.GetComponent<ShapeBox>();    //���̎Q��
    }


    //triggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�X�R�A���Z�̏���
        if(collision.tag == "Shape")   
        {
            if(collision.GetComponent<Shape>().ShapeType == box.ShapeType)  //�����̔��̎�ނƓ����}�`�����m�����ꍇ
            {
                //�X�R�A���Z����
                box.AddScore(); //�X�R�A���Z����

                //���Z��̏���
                BunbetsuManager.instance.ShapeToNotActive(collision.GetComponent<Shape>()); //�}�`������
            }
        }
    }
}
