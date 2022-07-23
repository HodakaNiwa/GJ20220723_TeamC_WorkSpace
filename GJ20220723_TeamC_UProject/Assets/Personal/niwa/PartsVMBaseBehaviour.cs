using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//==================================================================================
// <summary>
// �p�[�cVM
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class PartsVMBaseBehaviour : MonoBehaviour
{
    #region �C���X�y�N�^���J
    [SerializeField]
    private Sprite[] _sourceTextureArray = new Sprite[0];
    #endregion


    #region ���J�v���p�e�B
    public int SourceTexNum => _sourceTextureArray.Length;
    #endregion


    #region ����J�t�B�[���h
    private Image _vmComp = null;
    private int _nowDispTexIndex = 0;
    #endregion



    #region ���
    //==================================================================================
    // <summary>
    // ������
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        _vmComp = gameObject.GetComponent<Image>();
        SetSprite(_nowDispTexIndex);
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
    // VM�X�V
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public bool UpdateVM(int index)
    {
        if (!SetSprite(index))
        {
            return false;
        }
        return true;
    }
    #endregion


    #region ����J���\�b�h
    //==================================================================================
    // <summary>
    // �w��̃C���f�b�N�X�̃X�v���C�g�ɍX�V
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private bool SetSprite(int index)
    {
        if (index < 0 || index >= SourceTexNum)
        {
            return false;
        }
        if (_vmComp == null)
        {
            return false;
        }
        _vmComp.sprite = _sourceTextureArray[index];
        _nowDispTexIndex = index;

        return true;
    }
    #endregion
}
