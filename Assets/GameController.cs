using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    //定義
    public enum Scene { title = 0, playing = 1, result = 2} //場面を定義

    //シングルトン
    public static GameController instance;

    //参照
    [SerializeField] private BunbetsuManager bm;    //分別ゲームを管理するクラス
    [SerializeField] private GameObject titleCanvas;    //タイトルのキャンバス
    [SerializeField] private GameObject gameCanvas; //ゲームプレイのキャンバス
    [SerializeField] private GameObject resultCanvas;   //リザルトのキャンバス
    [SerializeField] private TextMeshProUGUI resultScoreText;   //リザルトのスコアのテキスト


    //宣言
    private Scene scene;    //場面


    //関数
    /// <summary>
    /// シーン遷移の処理
    /// </summary>
    /// <param name="newScene"></param>
    public void ChangeScene(int sceneNumber)
    {
        //シーン情報をあらかじめ設定しておき、読み込むことで増やすのが楽になる
        //タイトル場面
        if(sceneNumber == (int)Scene.title)
        {
            scene = Scene.title;    //場面変更
            titleCanvas.SetActive(true);    //タイトルキャンバスを表示
            gameCanvas.SetActive(false);    //ゲームキャンバスを非表示にする
            resultCanvas.SetActive(false);   //リザルトキャンバスの設定
        }

        //プレイ場面
        else if(sceneNumber == (int)Scene.playing)
        {
            scene = Scene.playing;    //場面変更
            titleCanvas.SetActive(false);    //タイトルキャンバスを非表示にする
            gameCanvas.SetActive(true);    //ゲームキャンバスを表示する
            resultCanvas.SetActive(false);   //リザルトキャンバスの設定

            //初期化処理
            bm.GameStart(); //ゲーム開始処理
        }

        //リザルト場面
        else if(sceneNumber == (int)Scene.result)
        {
            //場面設定
            scene = Scene.result;    //場面変更
            titleCanvas.SetActive(false);    //タイトルキャンバスを非表示にする
            gameCanvas.SetActive(false);    //ゲームキャンバスを非表示にする
            resultCanvas.SetActive(true);   //リザルトキャンバスの設定

            //リザルトの処理
            resultScoreText.text = "Score " + bm.GetScore().ToString();   //リザルトスコアを表示
        }

        //エラー
        else
        {
            Debug.Log("指定された場面は存在しません！");
        }
    }


    //awake
    private void Awake()
    {
        //シングルトン
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
        ChangeScene((int)Scene.title);  //場面設定
    }
}
