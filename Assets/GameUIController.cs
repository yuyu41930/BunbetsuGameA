using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    //参照
    [SerializeField] private Sprite heartImage; //ハートの画像
    [SerializeField] private RectTransform heartPivot;  //ハートの原点

    //宣言
    [SerializeField] private float heartDistance = 110f;    //ハート同士の距離


    //関数
    /// <summary>
    /// 体力の画像更新
    /// </summary>
    /// <param name="hpCount"></param>
    public void UpdateHPImage(int hpCount)
    {
        //ハート画像を全て破棄
        for(int count = heartPivot.childCount - 1; count >= 0; count--)
        {
            Destroy(heartPivot.GetChild(count).gameObject);    //ハート画像を破棄
        }

        //ハートを設置
        for(int count = 0; count < hpCount; count++)
        {
            //ハートを生成
            GameObject obj = new GameObject("HeartImage");  //オブジェクト生成
            RectTransform rect = obj.AddComponent<RectTransform>();  //RectTransformコンポーネントを追加
            Image image = obj.AddComponent<Image>();  //Imageコンポーネントを追加
            image.sprite = heartImage;  //画像にハートを設定

            //ハートを初期化
            rect.SetParent(heartPivot); //親を設定
            rect.localScale = Vector3.one; //大きさを初期化
            rect.anchoredPosition = Vector2.right * heartDistance * count;   //位置設定
        }
    }

    private void Start()
    {
        UpdateHPImage(3);
    }
}
