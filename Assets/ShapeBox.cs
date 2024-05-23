using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeBox : MonoBehaviour
{
    //参照
    [SerializeField] private BunbetsuManager bm;    //分別ゲームを管理するクラス
    [SerializeField] private TextMeshProUGUI scoreText; //スコアを表示するテキスト

    //宣言
    [SerializeField] private Shape.Type type;   //図形の種類
    private int score = 0;  //スコア

    //プロパティ
    public Shape.Type ShapeType { get { return type; } }    //図形の種類
    public int Score { get { return score; } }  //スコア


    //関数
    /// <summary>
    /// スコア加算の処理
    /// </summary>
    public void AddScore()
    {
        if(bm.CanGenerateShape) //図形生成可能フラグが立っている場合
        {
            //スコア加算の処理
            score++;    //スコア加算
            scoreText.text = score.ToString();  //スコアを表示
        }
    }
    /// <summary>
    /// スコアを初期化する処理
    /// </summary>
    public void RestScore()
    {
        score = 0;  //スコアを初期化
        scoreText.text = score.ToString();  //スコアを表示
    }
}
