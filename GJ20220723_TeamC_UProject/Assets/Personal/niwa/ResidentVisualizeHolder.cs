using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// �p�[�c�̃r�W���A���f�[�^
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class VisualData
{
    public GameObject[] PartsPrefab = new GameObject[0];
}

//==================================================================================
// <summary>
// �S�p�[�c�̃r�W���A���f�[�^�ێ�
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class ResidentVisualizeHolder : SingletonBaseBehaviour<ResidentVisualizeHolder>
{
    [SerializeField]
    private VisualData[] _visualDataArray = new VisualData[0];

    private int[] _partsIndexArray = new int[(int)ePARTS.Max];


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
    // �p�[�c�̃r�W���A���ԍ��擾
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
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
    // �p�[�c�̃r�W���A���ԍ��ݒ�
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
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
