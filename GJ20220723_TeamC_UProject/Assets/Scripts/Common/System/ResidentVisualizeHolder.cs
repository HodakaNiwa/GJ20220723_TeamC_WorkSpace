using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==================================================================================
// <summary>
// �S�p�[�c�̃r�W���A���f�[�^�ێ�
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class ResidentVisualizeHolder : SingletonBaseBehaviour<ResidentVisualizeHolder>
{
    //==================================================================================
    // <summary>
    // �p�[�c�̃r�W���A���f�[�^
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
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


    #region ���
    //==================================================================================
    // <summary>
    // ������
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // �Q�[���풓�I�u�W�F�N�g�Ƃ��ēo�^
        DontDestroyOnLoad(this);

        // �p�[�c�������o�^����(�Ƃ肠���������0)
        for (var i = 0; i < (int)ePARTS.Max; ++i)
        {
            _partsIndexArray[i] = 0;
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
    // �p�[�c�̃r�W���A���f�[�^�擾
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
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
    // �p�[�c�̃r�W���A���ԍ��ݒ�
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
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
    // �r�W���A���f�[�^�̓o�^
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public void RegisterVisualData(GameObject[] parts_array, int manageId)
    {
        var newData = new VisualData(manageId);
        newData.PartsPrefabs = parts_array;
        _visualDataList.Add(newData);
    }
    #endregion
}
