using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOctagon : MonoBehaviour
{
    // 回転の速さ
    [SerializeField]
    private float _RotateSpeed = 0.1f;

    void Update()
    {
        rotateOctagon();
    }


    /// <summary>
    /// 八角形を回転させる関数
    /// </summary>
    private void rotateOctagon()
    {
        this.transform.Rotate(0,0, _RotateSpeed * Time.deltaTime, Space.Self);
    }
}
