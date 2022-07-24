using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// �f�[�^�z���_�[
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public abstract class DataHolderBase : MonoBehaviour
{
    #region ���
    //==================================================================================
    // <summary>
    // ������
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        RegisterData();
        gameObject.SetActive(false);
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


    protected abstract void RegisterData();
}
