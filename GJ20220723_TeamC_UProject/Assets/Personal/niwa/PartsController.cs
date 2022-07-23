using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// �e�p�[�c�̒�`
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
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
// �e�p�[�c�̃G���g��
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
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
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class PartsController : MonoBehaviour
{
    [SerializeField]
    private string[] _partsObjectNameArray = new string[(int)ePARTS.Max];

    private Dictionary<int, PartsEntry> _partsEntryDict = new Dictionary<int, PartsEntry>();


    #region ���
    //==================================================================================
    // <summary>
    // ������
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // �Ƃ肠������U�q����S���T��
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

        // �e�p�[�c�𖼑O���猟������
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

                // �p�[�c�������ɓo�^
                var key = i + 1;
                _partsEntryDict.Add(key, new PartsEntry(child.gameObject));
                break;
            }
        }
    }

    //==================================================================================
    // <summary>
    // �X�V
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    void Update()
    {

    }
    #endregion


    #region ���J���\�b�h
    //==================================================================================
    // <summary>
    // �p�[�c�̎Q�Ǝ擾
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public bool TryGetPartsEntry(ePARTS partsEnum, out PartsEntry entry)
    {
        var key = (int)partsEnum + 1;
        return _partsEntryDict.TryGetValue(key, out entry);
    }


    //==================================================================================
    // <summary>
    // ���ׂẴp�[�c�Ƀ��[���b�g�̉�]�X�s�[�h��ݒ肷��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
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
