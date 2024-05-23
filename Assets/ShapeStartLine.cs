using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStartLine : MonoBehaviour
{
    //�錾
    [SerializeField] private float width;   //�o���ʒu�̕�


    //�֐�
    /// <summary>
    /// �}�`�������ʒu�ɐݒ肷�鏈��
    /// </summary>
    /// <param name="shape"></param>
    public void SetShapeToStartPos(Transform shape)
    {
        //�}�`�������ʒu�ɐݒ肷��
        shape.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-width / 2, width / 2), shape.position.z);
    }
}
