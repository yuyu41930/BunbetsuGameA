using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour
{
    //定義
    public enum Type { circle, square, triangle}    //形の種類
    private enum State { run, holded, fall, stop}         //図形の状態


    //参照
    private Camera m_cam; //カメラ
    private Rigidbody2D rb; //Rigidbody

    //変数
    private bool initialized = false;   //初期化フラグ
    [SerializeField] private Type type;  //形の種類
    private State state;    //状態
    [SerializeField] private float runSpeed = 5f;   //流れる速さ

    //プロパティ
    public Type ShapeType { get { return type; } }  //図形の種類


    //初期化
    /// <summary>
    /// 自身の初期化処理
    /// </summary>
    public void Initialize()
    {
        if(!initialized)    //初期化されていない場合
        {
            initialized = true; //初期化フラグを折る

            //初期化を行う
            //参照
            m_cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();  //カメラを取得
            rb = GetComponent<Rigidbody2D>();   //Rigidbody
            //初期化
            tag = "Shape";  //タグ設定
            ChangeState(State.run); //状態設定
        }
    }

    //図形自体の状態にかかわる関数
    /// <summary>
    /// 状態を変える処理
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(State newState)
    {
        //設定する値
        RigidbodyType2D rb_bodyType;    //Rigidbodyのモード

        //流れる状態の場合
        if(newState == State.run)
        {
            rb_bodyType = RigidbodyType2D.Kinematic;    //Rigidbodyの動き
        }
        //持たれる状態の場合
        else if(newState == State.holded)
        {
            rb_bodyType = RigidbodyType2D.Kinematic;    //Rigidbodyの動き
        }
        //落下状態の場合
        else if(newState == State.fall)
        {
            rb_bodyType = RigidbodyType2D.Dynamic;  //Rigidbodyの動き
        }
        //非活動状態の場合
        else
        {
            rb_bodyType = RigidbodyType2D.Kinematic;    //Rigidbodyの動き
        }

        //値を設定
        state = newState;   //図形の状態
        rb.bodyType = rb_bodyType;  //Rigidbodyのモード
    }
    /// <summary>
    /// 非活動状態にする処理
    /// </summary>
    public void ToNotActive()
    {
        ChangeState(State.stop);    //stop状態にする
    }
    /// <summary>
    /// 自身を初期状態に戻す
    /// </summary>
    public void ResetSelf()
    {
        ChangeState(State.run); //流れる状態になる
    }

    //クリック関連の関数
    /// <summary>
    /// クリック時の処理
    /// </summary>
    public void OnClieckDowned()
    {
        if(state == State.run || state == State.fall)   //流れるまたは落下状態の場合
        {
            //持たれる状態にする
            ChangeState(State.holded);  //状態変更
        }
    }
    /// <summary>
    /// ドラッグされた際呼ばれる
    /// </summary>
    public void OnDragged()
    {
        //ドラッグしている間、カーソルに付いて移動
        Vector3 mousePos = m_cam.ScreenToWorldPoint(Input.mousePosition); //マウス位置をワールド座標で取得
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z); //位置設定
    }
    /// <summary>
    /// ドラッグの終了時の処理
    /// </summary>
    /// <returns></returns>
    public void OnDropped()
    {
        //落下状態に変更
        ChangeState(State.fall);    
    }


    //update
    private void Update()
    {
        //流れる処理
        if(state == State.run)  
        {
            //左に流れる処理
            transform.position += new Vector3(-runSpeed * Time.deltaTime, 0f, 0f);   //移動
        }
    }
}
