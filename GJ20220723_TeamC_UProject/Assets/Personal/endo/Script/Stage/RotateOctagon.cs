using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOctagon : MonoBehaviour
{
    // ��]�̑���
    [SerializeField]
    private float _RotateSpeed = 0.1f;

    void Update()
    {
        rotateOctagon();
    }


    /// <summary>
    /// ���p�`����]������֐�
    /// </summary>
    private void rotateOctagon()
    {
        this.transform.Rotate(0,0, _RotateSpeed * Time.deltaTime, Space.Self);
    }
}
