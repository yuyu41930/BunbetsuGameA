using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeEndLine : MonoBehaviour
{
    //TriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EndLine�ɐG�ꂽ�}�`��StartLine�ɖ߂�����
        if(collision.tag == "Shape")    //�}�`�ƂԂ������ꍇ
        {
            //�~�X�̏���
            if(BunbetsuManager.instance.CanGenerateShape) //�}�`�����\�t���O�������Ă���ꍇ
            {
                BunbetsuManager.instance.Miss();  //�~�X�����ۂ̏���
            }

            BunbetsuManager.instance.ShapeToNotActive(collision.GetComponent<Shape>());   //�}�`������
        }
    }
}
