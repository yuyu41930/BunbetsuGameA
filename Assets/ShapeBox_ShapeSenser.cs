using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBox_ShapeSenser : MonoBehaviour
{
    //参照
    private ShapeBox box;   //図形の箱


    //start
    private void Start()
    {
        box = transform.parent.GetComponent<ShapeBox>();    //箱の参照
    }


    //triggerEnter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //スコア加算の処理
        if(collision.tag == "Shape")   
        {
            if(collision.GetComponent<Shape>().ShapeType == box.ShapeType)  //自分の箱の種類と同じ図形を感知した場合
            {
                //スコア加算処理
                box.AddScore(); //スコア加算処理

                //加算後の処理
                BunbetsuManager.instance.ShapeToNotActive(collision.GetComponent<Shape>()); //図形を消去
            }
        }
    }
}
