using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectBtn : MonoBehaviour
{
    // ���g�̃{�^����g�O��
    private Selectable _MySelectable;

    void Start()
    {
        _MySelectable = GetComponent<Selectable>();
    }


    /// <summary>
    /// �{�^����I�����邽�߂̊֐�
    /// </summary>
    public void setSelectable()
    {
        // �����L�[����������I�����ꂽ���̂��t�H�[�J�X����
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventSystem.current.SetSelectedGameObject(_MySelectable.navigation.selectOnRight.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventSystem.current.SetSelectedGameObject(_MySelectable.navigation.selectOnLeft.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventSystem.current.SetSelectedGameObject(_MySelectable.navigation.selectOnUp.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            EventSystem.current.SetSelectedGameObject(_MySelectable.navigation.selectOnDown.gameObject);
        }
    }
    
}
