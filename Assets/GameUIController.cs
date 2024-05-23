using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //�Q��
    [SerializeField] private Sprite heartImage; //�n�[�g�̉摜
    [SerializeField] private RectTransform heartPivot;  //�n�[�g�̌��_

    //�錾
    [SerializeField] private float heartDistance = 110f;    //�n�[�g���m�̋���


    //�֐�
    /// <summary>
    /// �̗͂̉摜�X�V
    /// </summary>
    /// <param name="hpCount"></param>
    public void UpdateHPImage(int hpCount)
    {
        //�n�[�g�摜��S�Ĕj��
        for(int count = heartPivot.childCount - 1; count >= 0; count--)
        {
            Destroy(heartPivot.GetChild(count).gameObject);    //�n�[�g�摜��j��
        }

        //�n�[�g��ݒu
        for(int count = 0; count < hpCount; count++)
        {
            //�n�[�g�𐶐�
            GameObject obj = new GameObject("HeartImage");  //�I�u�W�F�N�g����
            RectTransform rect = obj.AddComponent<RectTransform>();  //RectTransform�R���|�[�l���g��ǉ�
            Image image = obj.AddComponent<Image>();  //Image�R���|�[�l���g��ǉ�
            image.sprite = heartImage;  //�摜�Ƀn�[�g��ݒ�

            //�n�[�g��������
            rect.SetParent(heartPivot); //�e��ݒ�
            rect.localScale = Vector3.one; //�傫����������
            rect.anchoredPosition = Vector2.right * heartDistance * count;   //�ʒu�ݒ�
        }
    }

    private void Start()
    {
        UpdateHPImage(3);
    }
}
