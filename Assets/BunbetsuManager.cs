using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class BunbetsuManager : MonoBehaviour
{
    public static BunbetsuManager instance; //シングルトン

    //参照
    [SerializeField] private GameObject[] shapesPre;    //図形のプレハブ
    [SerializeField] private ShapeStartLine startLine;  //StartLine
    [SerializeField] private GameUIController uIController; //プレイ画面のUIの管理クラス
    [SerializeField] private ShapeBox[] shapeBoxes; //図形の箱

    //宣言
    private (Shape shape, bool active)[] objectPoolShapes;    //使いまわす図形
    [SerializeField] private int objectPoolShapes_shapeTypeCount;    //使いまわす図形の数
    [SerializeField] private int hp_max; //最大体力
    private int hp; //残り体力
    //図形生成の変数
    private bool canGenerateShape = false;  //図形生成可能状態  
    [SerializeField] private float shapeSpawnRate = 0.5f;   //図形生成間隔
    private float shapeSpawnRate_counter = 0f;  //生成間隔を数える変数

    //プロパティ
    public bool CanGenerateShape { get { return canGenerateShape; } }   //図形生成可能フラグ


    //図形のオブジェクトプールに関する関数
    /// <summary>
    /// 図形を取得する
    /// </summary>
    /// <returns></returns>
    private Shape GenerateShape(Shape.Type shapeType)
    {
        //図形を探す
        for(int count = 0; count < objectPoolShapes.Length; count++)
        {
            //図形発見
            if(!objectPoolShapes[count].active && objectPoolShapes[count].shape.ShapeType == shapeType)  //活動状態かつ引数と同じ図形を見つけた場合
            {
                objectPoolShapes[count].shape.gameObject.SetActive(true);   //図形オブジェクトをアクティブ化
                objectPoolShapes[count].active = true;  //図形を活動状態に設定
                return objectPoolShapes[count].shape;   //図形を返す
            }
        }

        //図形が見つからなかった場合、nullを返す
        Debug.Log("図形が見つかりません");
        return null;
    }
    /// <summary>
    /// 図形を非活動状態にする
    /// </summary>
    /// <param name="shape"></param>
    public void ShapeToNotActive(Shape shape)
    {
        //引数の図形と同じものを配列内から探す
        for(int count = 0; count < objectPoolShapes.Length; count++)
        {
            if(objectPoolShapes[count].active && objectPoolShapes[count].shape == shape)    //同じ図形が見つかった場合
            {
                //図形を非活動状態にする
                objectPoolShapes[count].shape.gameObject.SetActive(false);    //非アクティブ化
                objectPoolShapes[count].active = false; //状態を非活動状態に設定
                objectPoolShapes[count].shape.ToNotActive();  //図形の非活動状態にする処理
                return; //関数を終える
            }
        }

        //図形が見つからなかったらログを出す
        //Debug.Log("呼び出し元の関数名：" + new System.Diagnostics.StackFrame(1, false).GetMethod().Name+ "　非活動化する図形が見つかりません!");
    }

    //ゲームに関する関数
    /// <summary>
    /// ゲーム開始処理
    /// </summary>
    public void GameStart()
    {
        //初期化処理
        hp = hp_max;    //体力設定
        uIController.UpdateHPImage(hp); //体力を示すハートを表示
        foreach(ShapeBox box in shapeBoxes) box.RestScore();    //スコアを初期化

        //図形の初期化
        foreach(var shape in objectPoolShapes)  //全ての図形を初期化する
        {
            ShapeToNotActive(shape.shape);  //図形を非活動状態にする
        }

        //ゲーム開始処理
        canGenerateShape = true;    //図形生成フラグを立てる
    }
    /// <summary>
    /// ミスした際の処理
    /// </summary>
    public void Miss()
    {
        //体力を減らす
        hp--;
        uIController.UpdateHPImage(hp); //体力を表示する

        //ゲームオーバーの処理
        if(hp == 0) //体力が無い場合
        {
            GameOver(); //ゲームオーバー
        }
    }
    /// <summary>
    /// ゲームオーバーの処理
    /// </summary>
    public void GameOver()
    {
        //ゲーム終了の処理
        canGenerateShape = false;   //図形生成可能フラグを折る

        //リザルトへ移行
        int score = 0;
        foreach(ShapeBox box in shapeBoxes) //スコアを求める
        {
            score += box.Score; //スコア加算
        }
        GameController.instance.ChangeScene((int)GameController.Scene.result);  //リザルト場面へ移行
    }
    /// <summary>
    /// スコア取得処理
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
        //シングルトンの設定
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
        //図形生成
        objectPoolShapes = new (Shape, bool)[objectPoolShapes_shapeTypeCount * shapesPre.Length];   //配列を生成
        for(int typeCount = 0; typeCount < shapesPre.Length; typeCount++)    //図形の種類
        {
            for(int count = 0; count < objectPoolShapes_shapeTypeCount; count++)   //種類ごとの番号
            {
                //図形生成
                int arrayNum = typeCount * objectPoolShapes_shapeTypeCount + count;  //配列番号
                objectPoolShapes[arrayNum].shape = Instantiate(shapesPre[typeCount]).GetComponent<Shape>();  //図形を生成、配列に参照
                objectPoolShapes[arrayNum].shape.Initialize();  //図形の初期化
                objectPoolShapes[arrayNum].active = false;  //活動状態を初期化

                //図形を初期化
                objectPoolShapes[arrayNum].shape.gameObject.SetActive(false);   //図形を非アクティブ化
            }
        }
    }


    //update
    private void Update()
    {
        //図形生成に関連する処理
        if(canGenerateShape)
        {
            //図形の生成間隔を数える
            if(shapeSpawnRate_counter > 0f) //カウンターの値が０より大きい場合
            {
                shapeSpawnRate_counter -= Time.deltaTime;   //時間を数える
            }

            //図形生成
            if(shapeSpawnRate_counter <= 0f)    //カウンターの値が０以下の場合
            {
                //カウント時間を設定
                shapeSpawnRate_counter = shapeSpawnRate;

                //図形を生成、初期化
                Shape.Type shapeType = (Shape.Type)Enum.ToObject(typeof(Shape.Type),UnityEngine.Random.Range(0, shapesPre.Length)); //図形の種類をランダムに取得
                Shape shape = GenerateShape(shapeType); //図形を取得
                startLine.SetShapeToStartPos(shape.transform);  //図形を生成、初期化
                shape.ResetSelf(); //図形の初期化 
            }
        }
    }
}
