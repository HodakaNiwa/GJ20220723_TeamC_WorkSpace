using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivateButton : MonoBehaviour
{
    // デフォルトでフォーカスするボタン
    [SerializeField]
    private GameObject _FirstSelect;

    /// <summary>
    /// 選択したボタンが有効か無効化を確認する関数
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
