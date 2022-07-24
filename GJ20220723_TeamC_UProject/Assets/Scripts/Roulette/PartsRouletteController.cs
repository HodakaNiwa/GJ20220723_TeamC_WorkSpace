using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsRouletteController : MonoBehaviour
{
    public class RouletteEntry
    {
        public int BindedPrefabIndex = 0;
        public int ManageId { get; private set; } = 0;
        public PartsVMBaseBehaviour PartsVM { get; private set; } = null;

        public RouletteEntry(PartsVMBaseBehaviour partsVM, int manageId)
        {
            ManageId = manageId;
            PartsVM = partsVM;
        }
    }


    public int SourcePrefabNum { get; private set; }
    public int NextSwapPrefabIndex => _nextSwapPrefabIndex;


    [SerializeField]
    private GameObject[] _sourcePrefabArray = new GameObject[0];
    [SerializeField]
    private Vector3 _startPosition = Vector3.zero;


    private SpriteRenderer _frameRenderer = null;
    private SpriteMask _spriteMask = null;
    private PartsVMBaseBehaviour _currentVMComp = null;

    private List<RouletteEntry> _rouletteEntryList = new List<RouletteEntry>();
    private int _nextSwapPrefabIndex = 0;



    // Start is called before the first frame update
    void Start()
    {
        // スプライトの数を取得
        SourcePrefabNum = _sourcePrefabArray.Length;

        // 各種コンポーネント取得
        _spriteMask = gameObject.transform.Find("Mask").GetComponent<SpriteMask>();
        _frameRenderer = gameObject.transform.Find("Frame").GetComponent<SpriteRenderer>();
        _spriteMask.enabled = _frameRenderer.enabled = false;

        // 最初のパーツを生成
        var instance = GameObject.Instantiate(_sourcePrefabArray[0], transform);
        _currentVMComp = instance.GetComponent<PartsVMBaseBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region 公開メソッド
    //==================================================================================
    // <summary>
    // パーツを全部移動させる
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void MovePartsAll(RouletteBaseBehaviour.eMOVEDIRECTION direction, float moveValue)
    {
        foreach (var entry in _rouletteEntryList)
        {
            var position = entry.PartsVM.gameObject.transform.localPosition;
            switch (direction)
            {
                case RouletteBaseBehaviour.eMOVEDIRECTION.Vertical:
                    position.y += moveValue;
                    break;

                case RouletteBaseBehaviour.eMOVEDIRECTION.Horizontal:
                    position.x += moveValue;
                    break;
            }
            entry.PartsVM.gameObject.transform.localPosition = position;
        }
    }

    //==================================================================================
    // <summary>
    // パーツの場所を入れ替える
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SwapPartsPosition(RouletteBaseBehaviour.eMOVEDIRECTION direction, float offset)
    {
        _rouletteEntryList[_nextSwapPrefabIndex].PartsVM.transform.localPosition = _startPosition;
        _nextSwapPrefabIndex = (_nextSwapPrefabIndex - 1);
        if (_nextSwapPrefabIndex < 0)
        {
            _nextSwapPrefabIndex = _rouletteEntryList.Count - 1;
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
        _startPosition = _rouletteEntryList[0].PartsVM.gameObject.transform.localPosition;
        foreach (var entry in _rouletteEntryList)
        {
            var position = _startPosition;
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
            entry.PartsVM.transform.localPosition = position;
            nowOffset += offset;
        }
    }

    //==================================================================================
    // <summary>
    // 指定したインデックスのVMを取得
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public RouletteEntry GetRouletteEntry(int index)
    {
        return _rouletteEntryList[index];
    }

    //==================================================================================
    // <summary>
    // ルーレット用のパーツ生成
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void InstanciateRouletteParts(List<int> targetList)
    {
        _rouletteEntryList.Clear();
        var i = 0;
        foreach (var target in targetList)
        {
            var instance = GameObject.Instantiate(_sourcePrefabArray[target], transform);
            instance.gameObject.name = "roulette" + i.ToString();
            instance.gameObject.transform.localPosition = _startPosition;

            var vmBaseComp = instance.GetComponent<PartsVMBaseBehaviour>();
            vmBaseComp.InitVisible = false;
            vmBaseComp.InitSpliteMaskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            var newEntry = new RouletteEntry(vmBaseComp, i);
            newEntry.BindedPrefabIndex = target;
            _rouletteEntryList.Add(newEntry);
            ++i;
        }
    }

    //==================================================================================
    // <summary>
    // 最終的なパーツをSwapする
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SwapCurrentPartsResource(int index)
    {
        // もともとのパーツを削除
        var positon = _currentVMComp.gameObject.transform.localPosition;
        GameObject.Destroy(_currentVMComp.gameObject);

        // 新しくパーツを生成
        var instance = GameObject.Instantiate(_sourcePrefabArray[index], transform);
        instance.transform.localPosition = positon;
        _currentVMComp = instance.GetComponent<PartsVMBaseBehaviour>();
    }
    #endregion


    #region 非公開メソッド
    //==================================================================================
    // <summary>
    // スプライト表示切り替え
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void ChangeVisibleExceptingSprite(bool visible)
    {
        _currentVMComp.SetVisible(!visible);
        _spriteMask.enabled = _frameRenderer.enabled = visible;
        foreach (var entry in _rouletteEntryList)
        {
            entry.PartsVM.SetVisible(visible);
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
        if (_currentVMComp != null)
        {
            _currentVMComp.SetOrderInLayer(layer);
        }
        foreach (var entry in _rouletteEntryList)
        {
            entry.PartsVM.SetOrderInLayer(layer);
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
        if (_currentVMComp != null)
        {
            _currentVMComp.ResetDefaultOrderInLayer();
        }
        foreach (var entry in _rouletteEntryList)
        {
            entry.PartsVM.ResetDefaultOrderInLayer();
        }
    }
    #endregion
}
