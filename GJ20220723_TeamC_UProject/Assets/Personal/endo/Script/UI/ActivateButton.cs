using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivateButton : MonoBehaviour
{
    // �f�t�H���g�Ńt�H�[�J�X����{�^��
    [SerializeField]
    private GameObject _FirstSelect;

    /// <summary>
    /// �I�������{�^�����L�������������m�F����֐�
    /// </summary>
    public void isActivating(bool flag)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = flag;
        }

        if(flag)
        {
            EventSystem.current.SetSelectedGameObject(_FirstSelect);
        }
    }
}
