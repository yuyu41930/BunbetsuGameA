using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //��`
    public enum Scene { title = 0, playing = 1, result = 2} //��ʂ��`

    //�V���O���g��
    public static GameController instance;

    //�Q��
    [SerializeField] private BunbetsuManager bm;    //���ʃQ�[�����Ǘ�����N���X
    [SerializeField] private GameObject titleCanvas;    //�^�C�g���̃L�����o�X
    [SerializeField] private GameObject gameCanvas; //�Q�[���v���C�̃L�����o�X
    [SerializeField] private GameObject resultCanvas;   //���U���g�̃L�����o�X
    [SerializeField] private TextMeshProUGUI resultScoreText;   //���U���g�̃X�R�A�̃e�L�X�g


    //�錾
    private Scene scene;    //���


    //�֐�
    /// <summary>
    /// �V�[���J�ڂ̏���
    /// </summary>
    /// <param name="newScene"></param>
    public void ChangeScene(int sceneNumber)
    {
        //�V�[���������炩���ߐݒ肵�Ă����A�ǂݍ��ނ��Ƃő��₷�̂��y�ɂȂ�
        //�^�C�g�����
        if(sceneNumber == (int)Scene.title)
        {
            scene = Scene.title;    //��ʕύX
            titleCanvas.SetActive(true);    //�^�C�g���L�����o�X��\��
            gameCanvas.SetActive(false);    //�Q�[���L�����o�X���\���ɂ���
            resultCanvas.SetActive(false);   //���U���g�L�����o�X�̐ݒ�
        }

        //�v���C���
        else if(sceneNumber == (int)Scene.playing)
        {
            scene = Scene.playing;    //��ʕύX
            titleCanvas.SetActive(false);    //�^�C�g���L�����o�X���\���ɂ���
            gameCanvas.SetActive(true);    //�Q�[���L�����o�X��\������
            resultCanvas.SetActive(false);   //���U���g�L�����o�X�̐ݒ�

            //����������
            bm.GameStart(); //�Q�[���J�n����
        }

        //���U���g���
        else if(sceneNumber == (int)Scene.result)
        {
            //��ʐݒ�
            scene = Scene.result;    //��ʕύX
            titleCanvas.SetActive(false);    //�^�C�g���L�����o�X���\���ɂ���
            gameCanvas.SetActive(false);    //�Q�[���L�����o�X���\���ɂ���
            resultCanvas.SetActive(true);   //���U���g�L�����o�X�̐ݒ�

            //���U���g�̏���
            resultScoreText.text = "Score " + bm.GetScore().ToString();   //���U���g�X�R�A��\��
        }

        //�G���[
        else
        {
            Debug.Log("�w�肳�ꂽ��ʂ͑��݂��܂���I");
        }
    }


    //awake
    private void Awake()
    {
        //�V���O���g��
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
        ChangeScene((int)Scene.title);  //��ʐݒ�
    }
}
