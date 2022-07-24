using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==================================================================================
// <summary>
// 全パーツのビジュアルデータ保持
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class ResidentVisualizeHolder : SingletonBaseBehaviour<ResidentVisualizeHolder>
{
    //==================================================================================
    // <summary>
    // パーツのビジュアルデータ
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public class VisualData
    {
        public int ManageId { get; private set; } = -1;
        public GameObject[] PartsPrefabs;

        public VisualData(int manageId)
        {
            ManageId = manageId;
        }
    }

    private List<VisualData> _visualDataList = new List<VisualData>();

    [SerializeField]
    private int[] _partsIndexArray = new int[(int)ePARTS.Max];


    public int NextStageIndex = 0;
    public string[] StageScnNameArray;


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
    // パーツのビジュアルデータ取得
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public int GetPartsVisualDataIndex(ePARTS partsEnum)
    {
        var key = (int)partsEnum;
        if (key < 0 || key >= (int)ePARTS.Max)
        {
            return 0;
        }
        var index = _partsIndexArray[key];

        return index;
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
        if (key < 0 || key >= (int)ePARTS.Max)
        {
            return;
        }
        _partsIndexArray[key] = index;
    }

    //==================================================================================
    // <summary>
    // ビジュアルデータの登録
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void RegisterVisualData(GameObject[] parts_array, int manageId)
    {
        var newData = new VisualData(manageId);
        newData.PartsPrefabs = parts_array;
        _visualDataList.Add(newData);
    }
    #endregion
}
