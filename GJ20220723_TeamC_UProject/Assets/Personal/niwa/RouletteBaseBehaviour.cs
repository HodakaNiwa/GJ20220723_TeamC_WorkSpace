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

    /// <summary> ルーレットを回転させるか </summary>
    [SerializeField]
    protected bool _isRoulette = false;
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

    /// <summary> パーツのVM </summary>
    private PartsVMBaseBehaviour _partsVMComp = null;
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
        if (_elapsedCounter >= _rouletteRollTime)
        {
            _elapsedCounter = .0f;
            _selectedIdx = (_selectedIdx + 1) % _targetList.Count;
            _partsVMComp.UpdateVM(_selectedIdx);
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
    public bool StartRoulette()
    {
        // 対象となる番号リストを設定
        _targetList.Clear();

        // 対象が0の場合は処理しない
        if (_targetList.Count <= 0)
        {
            Debug.Log("対象がないため、ルーレット開始をスキップします");
            return false;
        }

        // フラグON
        _isRoulette = true;

        return true;
    }

    //==================================================================================
    // <summary>
    // ルーレット終了
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void EndRoulette()
    {
        // フラグOFF
        _isRoulette = false;

        // カウンターをリセット
        _elapsedCounter = .0f;

        Debug.Log("ルーレットストップ");
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
        // 念のためリストを初期化
        _targetList.Clear();

        // 外部ファイルから設定読み込み
        if (!LoadRouletteConfig())
        {
            Debug.Log("設定読み込み失敗");
            return false;
        }

        // 各種コンポーネントを取得
        _partsVMComp = GetComponent<PartsVMBaseBehaviour>();
        if (_partsVMComp != null)
        {
            for (var i = 0; i < _partsVMComp.SourceTexNum; ++i)
            {
                _targetList.Add(i);
            }
        }

        return true;
    }
    #endregion
}
