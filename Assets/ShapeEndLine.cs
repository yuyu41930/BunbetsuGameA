using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeEndLine : MonoBehaviour
{
    //TriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EndLineに触れた図形をStartLineに戻す処理
        if(collision.tag == "Shape")    //図形とぶつかった場合
        {
            //ミスの処理
            if(BunbetsuManager.instance.CanGenerateShape) //図形生成可能フラグが立っている場合
            {
                BunbetsuManager.instance.Miss();  //ミスした際の処理
            }

            BunbetsuManager.instance.ShapeToNotActive(collision.GetComponent<Shape>());   //図形を消去
        }
    }
}
