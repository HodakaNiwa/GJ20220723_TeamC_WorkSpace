using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==================================================================================
// <summary>
// パーツVM
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class PartsVMBaseBehaviour : MonoBehaviour
{
    #region インスペクタ公開
    [SerializeField]
    private Sprite[] _sourceTextureArray = new Sprite[0];
    #endregion


    #region 公開プロパティ
    public int SourceTexNum => _sourceTextureArray.Length;
    #endregion


    #region 非公開フィールド
    private Image _vmComp = null;
    private int _nowDispTexIndex = 0;
    #endregion



    #region 基底
    //==================================================================================
    // <summary>
    // 初期化
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        _vmComp = gameObject.GetComponent<Image>();
        SetSprite(_nowDispTexIndex);
    }

    //==================================================================================
    // <summary>
    // 更新
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    void Update()
    {

    }
    #endregion



    #region 公開メソッド
    //==================================================================================
    // <summary>
    // VM更新
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public bool UpdateVM(int index)
    {
        if (!SetSprite(index))
        {
            return false;
        }
        return true;
    }
    #endregion


    #region 非公開メソッド
    //==================================================================================
    // <summary>
    // 指定のインデックスのスプライトに更新
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private bool SetSprite(int index)
    {
        if (index < 0 || index >= SourceTexNum)
        {
            return false;
        }
        if (_vmComp == null)
        {
            return false;
        }
        _vmComp.sprite = _sourceTextureArray[index];
        _nowDispTexIndex = index;

        return true;
    }
    #endregion
}
