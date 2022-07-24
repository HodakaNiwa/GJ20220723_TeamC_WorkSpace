using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    // Box�̈ړ���
    [SerializeField]
    private float _MovableRange = 0.1f;

    void Update()
    {
        moveBox();
    }

    /// <summary>
    /// �X�e�[�W�̈ړ����̋���
    /// </summary>
    private void moveBox()
    {
        // ���݂̃{�b�N�X�̈ʒu���擾
        var _BoxPos = this.transform.position;

        // ����U��������p�̕ϐ�
        float sin = Mathf.Sin(Time.time);

        // ���W�X�V
        this.transform.position = new Vector3(_BoxPos.x + sin * _MovableRange, _BoxPos.y, 0.0f);
    }
}
