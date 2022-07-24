using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// パーツのビジュアルデータ
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class VisualData
{
    public GameObject[] PartsPrefab = new GameObject[0];
}

//==================================================================================
// <summary>
// 全パーツのビジュアルデータ保持
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class ResidentVisualizeHolder : SingletonBaseBehaviour<ResidentVisualizeHolder>
{
    [SerializeField]
    private VisualData[] _visualDataArray = new VisualData[0];

    private int[] _partsIndexArray = new int[(int)ePARTS.Max];


    #region 基底
    //==================================================================================
    // <summary>
    // 初期化
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // ゲーム常駐オブジェクトとして登録
        DontDestroyOnLoad(this);

        // パーツ分辞書登録する(とりあえず初回は0)
        for (var i = 0; i < (int)ePARTS.Max; ++i)
        {
            _partsIndexArray[i] = 0;
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
    //==================================================================================
    // <summary>
    // パーツのビジュアル番号取得
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public int GetPartsVisualDataIndex(ePARTS partsEnum)
    {
        var key = (int)partsEnum;
        if (key < 0 || key < (int)ePARTS.Max)
        {
            return 0;
        }

        return _partsIndexArray[key];
    }

    //==================================================================================
    // <summary>
    // パーツのビジュアル番号設定
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SetPartsVisualDataIndex(ePARTS partsEnum, int index)
    {
        var key = (int)partsEnum;
        if (key < 0 || key < (int)ePARTS.Max)
        {
            return;
        }
        _partsIndexArray[key] = index;
    }
    #endregion
}
