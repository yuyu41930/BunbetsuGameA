using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStartLine : MonoBehaviour
{
    //宣言
    [SerializeField] private float width;   //出現位置の幅


    //関数
    /// <summary>
    /// 図形を初期位置に設定する処理
    /// </summary>
    /// <param name="shape"></param>
    public void SetShapeToStartPos(Transform shape)
    {
        //図形を初期位置に設定する
        shape.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-width / 2, width / 2), shape.position.z);
    }
}
