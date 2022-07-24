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
    public class VMEntry
    {
        public int BindedTexIndex = 0;
        public int ManageId { get; private set; } = 0;
        public SpriteRenderer SpriteRenderer { get; private set; } = null;

        public VMEntry(SpriteRenderer spriteRenderer, int manageId)
        {
            ManageId = manageId;
            SpriteRenderer = spriteRenderer;
        }
    }


    #region インスペクタ公開
    [SerializeField]
    private Sprite[] _sourceTextureArray = new Sprite[0];
    #endregion


    #region 公開プロパティ
    public int SourceTexNum { get; private set; }

    public int NextSwapSpriteIndex => _nextSwapSpriteIndex;

    public SpriteRenderer CurrentRenderer => _currentRenderer;
    #endregion


    #region 非公開フィールド
    private SpriteRenderer _currentRenderer = null;
    private SpriteRenderer _frameRenderer = null;
    private SpriteMask _spriteMask = null;
    private List<VMEntry> _vmEntryList = new List<VMEntry>();
    private int _defaultOrderInLayer = 0;
    private Vector3 _endPosition = Vector3.zero;
    private int _nextSwapSpriteIndex = 0;
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
        // スプライトの数を取得
        SourceTexNum = _sourceTextureArray.Length;

        // 各種コンポーネント取得
        _currentRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteMask = gameObject.transform.Find("Mask").GetComponent<SpriteMask>();
        _frameRenderer = gameObject.transform.Find("Frame").GetComponent<SpriteRenderer>();
        _spriteMask.enabled = _frameRenderer.enabled = false;
        var tempRendererArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        var i = 0;
        foreach (var renderer in tempRendererArray)
        {
            if(_currentRenderer == renderer)
            {
                continue;
            }
            if (renderer.gameObject.name == "Frame")
            {
                continue;
            }
            var newEntry = new VMEntry(renderer, i);
            _vmEntryList.Add(newEntry);
            ++i;
        }

        // デフォルトの描画レイヤー番号を保存
        if (_currentRenderer != null)
        {
            _defaultOrderInLayer = _currentRenderer.sortingOrder;
        }

        // スプライトを事前に割り当てておく
        var index = 0;
        foreach (var vmEntry in _vmEntryList)
        {
            BindSpriteSource(index, vmEntry.SpriteRenderer);
            vmEntry.BindedTexIndex = index;
            index = (index + 1) % SourceTexNum;
        }
        _nextSwapSpriteIndex = _vmEntryList.Count - 1;

        // 初期化時点ではCurrent以外消す
        ChangeVisibleExceptingSprite(false);
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
    // スプライトを全部移動させる
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void MoveSpliteAll(RouletteBaseBehaviour.eMOVEDIRECTION direction, float moveValue)
    {
        foreach (var vmEntry in _vmEntryList)
        {
            var position = vmEntry.SpriteRenderer.gameObject.transform.localPosition;
            switch (direction)
            {
                case RouletteBaseBehaviour.eMOVEDIRECTION.Vertical:
                    position.y += moveValue;
                    break;

                case RouletteBaseBehaviour.eMOVEDIRECTION.Horizontal:
                    position.x += moveValue;
                    break;
            }
            vmEntry.SpriteRenderer.gameObject.transform.localPosition = position;
        }
    }

    //==================================================================================
    // <summary>
    // スプライトの場所を入れ替える
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SwapSplitePosition(RouletteBaseBehaviour.eMOVEDIRECTION direction, float offset)
    {
        _vmEntryList[_nextSwapSpriteIndex].SpriteRenderer.transform.localPosition = _endPosition;
        _nextSwapSpriteIndex = (_nextSwapSpriteIndex - 1 );
        if (_nextSwapSpriteIndex < 0)
        {
            _nextSwapSpriteIndex = _vmEntryList.Count - 1;
        }
    }

    //==================================================================================
    // <summary>
    // ルーレット素材パーツのオフセットを設定
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SetRoulettePartsOffset(RouletteBaseBehaviour.eMOVEDIRECTION direction, float offset)
    {
        var nowOffset = .0f;
        _endPosition = _vmEntryList[0].SpriteRenderer.gameObject.transform.localPosition;
        foreach (var vmEntry in _vmEntryList)
        {
            var position = _endPosition;
            switch (direction)
            {
                case RouletteBaseBehaviour.eMOVEDIRECTION.Vertical:
                    position.x = .0f;
                    position.y += nowOffset;
                    break;

                case RouletteBaseBehaviour.eMOVEDIRECTION.Horizontal:
                    position.x += nowOffset;
                    position.y = .0f;
                    break;
            }
            vmEntry.SpriteRenderer.transform.localPosition = position;
            nowOffset += offset;
        }
    }

    //==================================================================================
    // <summary>
    // 指定したインデックスのVMを取得
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public VMEntry GetVMEntry(int index)
    {
        return _vmEntryList[index];
    }

    //==================================================================================
    // <summary>
    // 指定のインデックスのリソースがどの番号のVMに割り当たっているのか取得
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public int GetTargetIndexFromVMEntry(int texIndex)
    {
        for (var i = 0; i < _vmEntryList.Count; ++i)
        {
            if (_vmEntryList[i].BindedTexIndex == texIndex)
            {
                return i;
            }
        }
        return 0;
    }


    //==================================================================================
    // <summary>
    // CurrentVMに指定インデックスのリソース割り当て
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void BindResourceToCurrenVM(int index)
    {
        BindSpriteSource(index, _currentRenderer);
    }
    #endregion


    #region 非公開メソッド
    //==================================================================================
    // <summary>
    // 指定のインデックスのスプライトリソースを割り当て
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private bool BindSpriteSource(int index, SpriteRenderer renderer)
    {
        if (index < 0 || index >= SourceTexNum)
        {
            return false;
        }
        if (renderer == null)
        {
            return false;
        }
        renderer.sprite = _sourceTextureArray[index];
        return true;
    }

    //==================================================================================
    // <summary>
    // スプライト表示切り替え
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void ChangeVisibleExceptingSprite(bool visible)
    {
        _currentRenderer.enabled = !visible;
        _spriteMask.enabled = _frameRenderer.enabled = visible;
        foreach (var vmEntry in _vmEntryList)
        {
            vmEntry.SpriteRenderer.enabled = visible;
        }
    }

    //==================================================================================
    // <summary>
    // スプライトの描画順を設定
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void ChangeSpliteOrderInLayer(int layer)
    {
        if (_currentRenderer != null)
        {
            _currentRenderer.sortingOrder = layer;
        }
        foreach (var vmEntry in _vmEntryList)
        {
            vmEntry.SpriteRenderer.sortingOrder = layer;
        }
    }

    //==================================================================================
    // <summary>
    // スプライトの描画順をデフォルトに戻す
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void ResetSpliteOrderInLayer()
    {
        if (_currentRenderer != null)
        {
            _currentRenderer.sortingOrder = _defaultOrderInLayer;
        }

        foreach (var vmEntry in _vmEntryList)
        {
            vmEntry.SpriteRenderer.sortingOrder = _defaultOrderInLayer;
        }
    }
    #endregion
}
