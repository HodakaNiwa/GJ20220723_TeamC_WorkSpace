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
    public bool InitVisible = true;
    public SpriteMaskInteraction InitSpliteMaskInteraction = SpriteMaskInteraction.None;

    #region 非公開フィールド
    private List<PartsSprite> _partsSpriteList = new List<PartsSprite>();
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
        // 各種コンポーネント取得
        _partsSpriteList.Clear();
        var compArray = gameObject.GetComponentsInChildren<PartsSprite>();
        foreach (var comp in compArray)
        {
            comp.Init();
            comp.MaskInteraction = InitSpliteMaskInteraction;
            comp.enabled = InitVisible;
            _partsSpriteList.Add(comp);
        }
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
    public void SetOrderInLayer(int layer)
    {
        foreach (var sprite in _partsSpriteList)
        {
            sprite.SetOrderInLayer(layer + sprite.DefaultOrderInLayer);
        }
    }

    public void ResetDefaultOrderInLayer()
    {
        foreach (var sprite in _partsSpriteList)
        {
            sprite.SetOrderInLayer(sprite.DefaultOrderInLayer);
        }
    }

    public void SetVisible(bool visible)
    {
        foreach (var sprite in _partsSpriteList)
        {
            sprite.SetVisible(visible);
        }
    }

    public void SetMaskInteraction(SpriteMaskInteraction mask)
    {
        foreach (var sprite in _partsSpriteList)
        {
            sprite.MaskInteraction = mask;
        }
    }
    #endregion
}
