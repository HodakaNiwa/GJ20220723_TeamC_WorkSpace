using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateUIBtn : MonoBehaviour
{
    // 画面UI
    [SerializeField]
    private GameObject _StatusWindow;

    // ボタンの有効・無効について書かれているクラス型
    [SerializeField]
    private ActivateButton _Select;

    void Update()
    {
        //　スペースキーを押したら画面UIのオン・オフ
        if (Input.GetKeyDown("space"))
        {
            _StatusWindow.SetActive(!_StatusWindow.activeSelf);

            //　画面を開いた時にBackground1のボタンのインタラクティブをtrue、Background2のボタンのインタラクティブをfalseにする
            if (_StatusWindow.activeSelf)
            {

                _Select.isActivating(true);

                //　画面を閉じたら選択を解除
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

    }
}
