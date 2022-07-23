using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// Šeƒp[ƒc‚Ì’è‹`
// </summary>
// <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
//==================================================================================
public enum ePARTS
{
    Invalid = -1,

    Head,
    Body,
    Hand_L,
    Hand_R,
    Leg_L,
    Leg_R,
    Ex_Boost,

    Max,
    Start = Head,
    End = Max - 1,
}

//==================================================================================
// <summary>
// Šeƒp[ƒc‚ÌƒGƒ“ƒgƒŠ
// </summary>
// <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
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
// <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
//==================================================================================
public class PartsController : MonoBehaviour
{
    private readonly string[] _partsObjectNameArray =
    {
        "head",
        "body",
        "hand_L",
        "hand_R",
        "leg_L",
        "leg_R",
        "boost",
    };

    private Dictionary<int, PartsEntry> _partsEntryDict = new Dictionary<int, PartsEntry>();


    #region Šî’ê
    //==================================================================================
    // <summary>
    // ‰Šú‰»
    // </summary>
    // <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // ‚Æ‚è‚ ‚¦‚¸ˆê’Uq‹Ÿ‚ğ‘S•”’T‚·
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

        // Šeƒp[ƒc‚ğ–¼‘O‚©‚çŒŸõ‚·‚é
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

                // ƒp[ƒc‚ğ«‘‚É“o˜^
                var key = i + 1;
                _partsEntryDict.Add(key, new PartsEntry(child.gameObject));
                break;
            }
        }
    }

    //==================================================================================
    // <summary>
    // XV
    // </summary>
    // <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
    //==================================================================================
    void Update()
    {

    }
    #endregion


    #region ŒöŠJƒƒ\ƒbƒh
    //==================================================================================
    // <summary>
    // ƒp[ƒc‚ÌQÆæ“¾
    // </summary>
    // <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
    //==================================================================================
    public bool TryGetPartsEntry(ePARTS partsEnum, out PartsEntry entry)
    {
        var key = (int)partsEnum + 1;
        return _partsEntryDict.TryGetValue(key, out entry);
    }
    #endregion
}
