//==================================================================================
// <summary>
// RouletteBaseBehaviour.cs
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// ルーレット処理基底
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class RouletteBaseBehaviour : MonoBehaviour
{
    public enum eMOVEDIRECTION
    {
        Invalid = -1,

        Vertical,
        Horizontal,

        Max,

        Start = Vertical,
        End = Max - 1,
    }


    #region 公開プロパティ
    /// <summary> 選択された番号 </summary>
    public int SelectedIdx => (int)_targetList[_selectedIdx];

    /// <summary> 現在ルーレット中か </summary>
    public bool IsRoulette => _isRoulette;
    #endregion


    #region 公開フィールド
    /// <summary> ルーレットが回転する秒数 </summary>
    [SerializeField]
    protected float _rouletteRollTime = .0f;

    /// <summary> ルーレットが回転中か </summary>
    [SerializeField]
    protected bool _isRoulette = false;

    /// <summary> パーツ同士のオフセット </summary>
    [SerializeField]
    private float _rouletteOffset = .0f;

    /// <summary> ルーレットの回転方向 </summary>
    [SerializeField]
    private eMOVEDIRECTION _rouletteMoveDirection = eMOVEDIRECTION.Vertical;

    /// <summary> ルーレットの終了リクエストがされているか </summary>
    private bool _isStopRouletteRequest = false;
    #endregion


    #region 非公開フィールド
    /// <summary> 選択された番号 </summary>
    [SerializeField]
    private int _selectedIdx = 0;

    /// <summary> ルーレット対象となる番号 </summary>
    [SerializeField]
    private List<int> _targetList = new List<int>();

    /// <summary> 時間計測用カウンター </summary>
    private float _elapsedCounter = .0f;

    /// <summary> パーツのルーレットコントローラー </summary>
    private PartsRouletteController _partsRouletteController = null;

    private int _targetPartsIndex = 0;
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
        if (!Setup())
        {
            Debug.Log("セットアップ失敗！");
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
        // 回転フラグが折れていたら処理しない
        if (!_isRoulette)
        {
            return;
        }

        // カウンターを加算し、一定値に達したらランダムで選択番号設定
        _elapsedCounter += Time.deltaTime;
        var moveValue = (_rouletteOffset / _rouletteRollTime) * Time.deltaTime;
        _partsRouletteController.MovePartsAll(_rouletteMoveDirection, moveValue);
        if (_elapsedCounter >= _rouletteRollTime)
        {
            _elapsedCounter = .0f;
            if (_isStopRouletteRequest)
            {
                StopRoulette();
                return;
            }
            else
            {
                _partsRouletteController.SwapPartsPosition(_rouletteMoveDirection, _rouletteOffset);
            }
        }
    }
    #endregion


    #region 継承想定メソッド
    //==================================================================================
    // <summary>
    // 設定ファイル読み込み
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public virtual bool LoadRouletteConfig() { return true; }
    #endregion


    #region 公開メソッド
    //==================================================================================
    // <summary>
    // ルーレット開始
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public bool StartRoulette(int layer)
    {
        // 対象が0の場合は処理しない
        if (_targetList.Count <= 0)
        {
            Debug.Log("対象がないため、ルーレット開始をスキップします");
            return false;
        }

        // フラグON
        _isRoulette = true;
        _partsRouletteController.ChangeVisibleExceptingSprite(true);
        _partsRouletteController.ChangeSpliteOrderInLayer(layer);

        return true;
    }

    //==================================================================================
    // <summary>
    // ルーレット終了
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void RequestStopRoulette()
    {
        // フラグOFF
        _isStopRouletteRequest = true;

        // ターゲットパーツの番号を取得
        var currentVMIndex = _partsRouletteController.NextSwapPrefabIndex;
        for (var i = 0; i < 2; ++i)
        {
            currentVMIndex = currentVMIndex - 1;
            if (currentVMIndex < 0)
            {
                currentVMIndex = _partsRouletteController.SourcePrefabNum - 1;
            }
        }
        _targetPartsIndex = currentVMIndex;
        var entry = _partsRouletteController.GetRouletteEntry(currentVMIndex);
        if (entry != null)
        {
            _selectedIdx = entry.BindedPrefabIndex;
        }
    }

    //==================================================================================
    // <summary>
    // ルーレットの回転スピード設定
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SetRouletteRollSpeed(float speed)
    {
        _rouletteRollTime = speed;
    }
    #endregion


    #region 非公開メソッド
    //==================================================================================
    // <summary>
    // セットアップ
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private bool Setup()
    {
        // 各種コンポーネントを取得
        _partsRouletteController = GetComponent<PartsRouletteController>();
        if (_partsRouletteController != null)
        {
            // パーツをInstanciateする
            _partsRouletteController.InstanciateRouletteParts(_targetList);

            // ルーレットのパーツ素材オフセットを設定
            _partsRouletteController.SetRoulettePartsOffset(_rouletteMoveDirection, _rouletteOffset);
        }

        return true;
    }

    //==================================================================================
    // <summary>
    // ルーレット停止
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void StopRoulette()
    {
        // フラグOFF
        _isRoulette = false;
        _isStopRouletteRequest = false;
        _partsRouletteController.ChangeVisibleExceptingSprite(false);
        _partsRouletteController.ResetSpliteOrderInLayer();
        _partsRouletteController.SwapCurrentPartsResource(_selectedIdx);

        // カウンターをリセット
        _elapsedCounter = .0f;
    }
    #endregion
}
