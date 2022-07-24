using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    // Boxの移動幅
    [SerializeField]
    private float _MovableRange = 0.1f;

    void Update()
    {
        moveBox();
    }

    /// <summary>
    /// ステージの移動床の挙動
    /// </summary>
    private void moveBox()
    {
        // 現在のボックスの位置を取得
        var _BoxPos = this.transform.position;

        // 床を振動させる用の変数
        float sin = Mathf.Sin(Time.time);

        // 座標更新
        this.transform.position = new Vector3(_BoxPos.x + sin * _MovableRange, _BoxPos.y, 0.0f);
    }
}
