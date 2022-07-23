using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// 各パーツの定義
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public enum ePARTS
{
    Invalid = -1,

    Head_Body,
    Hand_L,
    Hand_R,
    Leg_L,
    Leg_R,
    Ex_Boost,

    Max,
    Start = Head_Body,
    End = Max - 1,
}

//==================================================================================
// <summary>
// 各パーツのエントリ
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class PartsEntry
{
    public GameObject PartsObject { get; private set; } = null;
    public RouletteBaseBehaviour RouletteComp { get; private set; } = null;

    public PartsEntry(GameObject partsObject)
    {
        PartsObject = partsObject;
        RouletteComp = PartsObject.GetComponent<RouletteBaseBehaviour>();
    }
}


//==================================================================================
// <summary>
// RouletteBaseBehaviour.cs
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class PartsController : MonoBehaviour
{
    [SerializeField]
    private string[] _partsObjectNameArray = new string[(int)ePARTS.Max];

    private Dictionary<int, PartsEntry> _partsEntryDict = new Dictionary<int, PartsEntry>();


    #region 基底
    //==================================================================================
    // <summary>
    // 初期化
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // とりあえず一旦子供を全部探す
        var childList = new List<Transform>();
        for (var i = 0; i < (int)ePARTS.Max; ++i)
        {
            var child = gameObject.transform.GetChild(i);
            if (child == null)
            {
                continue;
            }
            childList.Add(child);
        }

        // 各パーツを名前から検索する
        _partsEntryDict.Clear();
        var addedList = new List<int>();
        for (var i = 0; i < (int)ePARTS.Max; ++i)
        {
            foreach (var child in childList)
            {
                if(child.name != _partsObjectNameArray[i])
                {
                    continue;
                }
                if (addedList.Contains(i))
                {
                    continue;
                }
                addedList.Add(i);

                // パーツを辞書に登録
                var key = i + 1;
                _partsEntryDict.Add(key, new PartsEntry(child.gameObject));
                break;
            }
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
    // パーツの参照取得
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public bool TryGetPartsEntry(ePARTS partsEnum, out PartsEntry entry)
    {
        var key = (int)partsEnum + 1;
        return _partsEntryDict.TryGetValue(key, out entry);
    }


    //==================================================================================
    // <summary>
    // すべてのパーツにルーレットの回転スピードを設定する
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    public void SetRouletteSppedOnAllPartsEntry(float speed)
    {
        foreach (var keyValuePair in _partsEntryDict)
        {
            keyValuePair.Value.RouletteComp.SetRouletteRollSpeed(speed);

        }
    }
    #endregion
}
