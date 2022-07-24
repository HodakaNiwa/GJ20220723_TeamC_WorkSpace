using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateUIBtn : MonoBehaviour
{
    // ���UI
    [SerializeField]
    private GameObject _StatusWindow;

    // �{�^���̗L���E�����ɂ��ď�����Ă���N���X�^
    [SerializeField]
    private ActivateButton _Select;

    void Update()
    {
        //�@�X�y�[�X�L�[������������UI�̃I���E�I�t
        if (Input.GetKeyDown("space"))
        {
            _StatusWindow.SetActive(!_StatusWindow.activeSelf);

            //�@��ʂ��J��������Background1�̃{�^���̃C���^���N�e�B�u��true�ABackground2�̃{�^���̃C���^���N�e�B�u��false�ɂ���
            if (_StatusWindow.activeSelf)
            {

                _Select.isActivating(true);

                //�@��ʂ������I��������
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

    }
}
