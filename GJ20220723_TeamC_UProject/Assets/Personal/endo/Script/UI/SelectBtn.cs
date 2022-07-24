using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectBtn : MonoBehaviour
{
    // 自身のボタンやトグル
    private Selectable _MySelectable;

    void Start()
    {
        _MySelectable = GetComponent<Selectable>();
    }


    /// <summary>
    /// ボタンを選択するための関数
    /// </summary>
    public void setSelectable()
    {
        // 方向キーを押したら選択されたものをフォーカスする
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
