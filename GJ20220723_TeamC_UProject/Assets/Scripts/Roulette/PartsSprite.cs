using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSprite : MonoBehaviour
{
    #region 公開プロパティ
    public int DefaultOrderInLayer => _defaultOrderInLayer;

    public SpriteMaskInteraction MaskInteraction
    {
        get
        {
            return _renderer.maskInteraction;
        }
        set
        {
            _renderer.maskInteraction = value;
        }
    }
    #endregion


    #region 非公開フィールド
    private SpriteRenderer _renderer = null;
    private int _defaultOrderInLayer = 0;
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
    public void Init()
    {
        // 各種コンポーネント取得
        _renderer = gameObject.GetComponent<SpriteRenderer>();

        if (_renderer != null)
        {
            // デフォルトの描画レイヤー番号を保存
            _defaultOrderInLayer = _renderer.sortingOrder;
        }
    }

    public void SetOrderInLayer(int layer)
    {
        _renderer.sortingOrder = layer;
    }

    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
    }
    #endregion
}
