using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//==================================================================================
// <summary>
// Startup.cs
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class Startup : MonoBehaviour
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
        // ��������΂�����
    }

    //==================================================================================
    // <summary>
    // �X�V
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    void Update()
    {
        SceneManager.LoadScene("TestRouletteScn_niwa");
    }
    #endregion
}
