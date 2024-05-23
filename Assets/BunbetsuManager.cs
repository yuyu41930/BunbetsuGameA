using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class BunbetsuManager : MonoBehaviour
{
    public static BunbetsuManager instance; //�V���O���g��

    //�Q��
    [SerializeField] private GameObject[] shapesPre;    //�}�`�̃v���n�u
    [SerializeField] private ShapeStartLine startLine;  //StartLine
    [SerializeField] private GameUIController uIController; //�v���C��ʂ�UI�̊Ǘ��N���X
    [SerializeField] private ShapeBox[] shapeBoxes; //�}�`�̔�

    //�錾
    private (Shape shape, bool active)[] objectPoolShapes;    //�g���܂킷�}�`
    [SerializeField] private int objectPoolShapes_shapeTypeCount;    //�g���܂킷�}�`�̐�
    [SerializeField] private int hp_max; //�ő�̗�
    private int hp; //�c��̗�
    //�}�`�����̕ϐ�
    private bool canGenerateShape = false;  //�}�`�����\���  
    [SerializeField] private float shapeSpawnRate = 0.5f;   //�}�`�����Ԋu
    private float shapeSpawnRate_counter = 0f;  //�����Ԋu�𐔂���ϐ�

    //�v���p�e�B
    public bool CanGenerateShape { get { return canGenerateShape; } }   //�}�`�����\�t���O


    //�}�`�̃I�u�W�F�N�g�v�[���Ɋւ���֐�
    /// <summary>
    /// �}�`���擾����
    /// </summary>
    /// <returns></returns>
    private Shape GenerateShape(Shape.Type shapeType)
    {
        //�}�`��T��
        for(int count = 0; count < objectPoolShapes.Length; count++)
        {
            //�}�`����
            if(!objectPoolShapes[count].active && objectPoolShapes[count].shape.ShapeType == shapeType)  //������Ԃ������Ɠ����}�`���������ꍇ
            {
                objectPoolShapes[count].shape.gameObject.SetActive(true);   //�}�`�I�u�W�F�N�g���A�N�e�B�u��
                objectPoolShapes[count].active = true;  //�}�`��������Ԃɐݒ�
                return objectPoolShapes[count].shape;   //�}�`��Ԃ�
            }
        }

        //�}�`��������Ȃ������ꍇ�Anull��Ԃ�
        Debug.Log("�}�`��������܂���");
        return null;
    }
    /// <summary>
    /// �}�`��񊈓���Ԃɂ���
    /// </summary>
    /// <param name="shape"></param>
    public void ShapeToNotActive(Shape shape)
    {
        //�����̐}�`�Ɠ������̂�z�������T��
        for(int count = 0; count < objectPoolShapes.Length; count++)
        {
            if(objectPoolShapes[count].active && objectPoolShapes[count].shape == shape)    //�����}�`�����������ꍇ
            {
                //�}�`��񊈓���Ԃɂ���
                objectPoolShapes[count].shape.gameObject.SetActive(false);    //��A�N�e�B�u��
                objectPoolShapes[count].active = false; //��Ԃ�񊈓���Ԃɐݒ�
                objectPoolShapes[count].shape.ToNotActive();  //�}�`�̔񊈓���Ԃɂ��鏈��
                return; //�֐����I����
            }
        }

        //�}�`��������Ȃ������烍�O���o��
        //Debug.Log("�Ăяo�����̊֐����F" + new System.Diagnostics.StackFrame(1, false).GetMethod().Name+ "�@�񊈓�������}�`��������܂���!");
    }

    //�Q�[���Ɋւ���֐�
    /// <summary>
    /// �Q�[���J�n����
    /// </summary>
    public void GameStart()
    {
        //����������
        hp = hp_max;    //�̗͐ݒ�
        uIController.UpdateHPImage(hp); //�̗͂������n�[�g��\��
        foreach(ShapeBox box in shapeBoxes) box.RestScore();    //�X�R�A��������

        //�}�`�̏�����
        foreach(var shape in objectPoolShapes)  //�S�Ă̐}�`������������
        {
            ShapeToNotActive(shape.shape);  //�}�`��񊈓���Ԃɂ���
        }

        //�Q�[���J�n����
        canGenerateShape = true;    //�}�`�����t���O�𗧂Ă�
    }
    /// <summary>
    /// �~�X�����ۂ̏���
    /// </summary>
    public void Miss()
    {
        //�̗͂����炷
        hp--;
        uIController.UpdateHPImage(hp); //�̗͂�\������

        //�Q�[���I�[�o�[�̏���
        if(hp == 0) //�̗͂������ꍇ
        {
            GameOver(); //�Q�[���I�[�o�[
        }
    }
    /// <summary>
    /// �Q�[���I�[�o�[�̏���
    /// </summary>
    public void GameOver()
    {
        //�Q�[���I���̏���
        canGenerateShape = false;   //�}�`�����\�t���O��܂�

        //���U���g�ֈڍs
        int score = 0;
        foreach(ShapeBox box in shapeBoxes) //�X�R�A�����߂�
        {
            score += box.Score; //�X�R�A���Z
        }
        GameController.instance.ChangeScene((int)GameController.Scene.result);  //���U���g��ʂֈڍs
    }
    /// <summary>
    /// �X�R�A�擾����
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        int score = 0;
        foreach(ShapeBox box in shapeBoxes)
        {
            score += box.Score;
        }
        return score;
    }


    //awake
    private void Awake()
    {
        //�V���O���g���̐ݒ�
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    //start
    private void Start()
    {
        //�}�`����
        objectPoolShapes = new (Shape, bool)[objectPoolShapes_shapeTypeCount * shapesPre.Length];   //�z��𐶐�
        for(int typeCount = 0; typeCount < shapesPre.Length; typeCount++)    //�}�`�̎��
        {
            for(int count = 0; count < objectPoolShapes_shapeTypeCount; count++)   //��ނ��Ƃ̔ԍ�
            {
                //�}�`����
                int arrayNum = typeCount * objectPoolShapes_shapeTypeCount + count;  //�z��ԍ�
                objectPoolShapes[arrayNum].shape = Instantiate(shapesPre[typeCount]).GetComponent<Shape>();  //�}�`�𐶐��A�z��ɎQ��
                objectPoolShapes[arrayNum].shape.Initialize();  //�}�`�̏�����
                objectPoolShapes[arrayNum].active = false;  //������Ԃ�������

                //�}�`��������
                objectPoolShapes[arrayNum].shape.gameObject.SetActive(false);   //�}�`���A�N�e�B�u��
            }
        }
    }


    //update
    private void Update()
    {
        //�}�`�����Ɋ֘A���鏈��
        if(canGenerateShape)
        {
            //�}�`�̐����Ԋu�𐔂���
            if(shapeSpawnRate_counter > 0f) //�J�E���^�[�̒l���O���傫���ꍇ
            {
                shapeSpawnRate_counter -= Time.deltaTime;   //���Ԃ𐔂���
            }

            //�}�`����
            if(shapeSpawnRate_counter <= 0f)    //�J�E���^�[�̒l���O�ȉ��̏ꍇ
            {
                //�J�E���g���Ԃ�ݒ�
                shapeSpawnRate_counter = shapeSpawnRate;

                //�}�`�𐶐��A������
                Shape.Type shapeType = (Shape.Type)Enum.ToObject(typeof(Shape.Type),UnityEngine.Random.Range(0, shapesPre.Length)); //�}�`�̎�ނ������_���Ɏ擾
                Shape shape = GenerateShape(shapeType); //�}�`���擾
                startLine.SetShapeToStartPos(shape.transform);  //�}�`�𐶐��A������
                shape.ResetSelf(); //�}�`�̏����� 
            }
        }
    }
}
