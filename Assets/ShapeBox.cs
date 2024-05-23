using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeBox : MonoBehaviour
{
    //�Q��
    [SerializeField] private BunbetsuManager bm;    //���ʃQ�[�����Ǘ�����N���X
    [SerializeField] private TextMeshProUGUI scoreText; //�X�R�A��\������e�L�X�g

    //�錾
    [SerializeField] private Shape.Type type;   //�}�`�̎��
    private int score = 0;  //�X�R�A

    //�v���p�e�B
    public Shape.Type ShapeType { get { return type; } }    //�}�`�̎��
    public int Score { get { return score; } }  //�X�R�A


    //�֐�
    /// <summary>
    /// �X�R�A���Z�̏���
    /// </summary>
    public void AddScore()
    {
        if(bm.CanGenerateShape) //�}�`�����\�t���O�������Ă���ꍇ
        {
            //�X�R�A���Z�̏���
            score++;    //�X�R�A���Z
            scoreText.text = score.ToString();  //�X�R�A��\��
        }
    }
    /// <summary>
    /// �X�R�A�����������鏈��
    /// </summary>
    public void RestScore()
    {
        score = 0;  //�X�R�A��������
        scoreText.text = score.ToString();  //�X�R�A��\��
    }
}
