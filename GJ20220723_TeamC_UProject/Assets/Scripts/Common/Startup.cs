using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//==================================================================================
// <summary>
// Startup.cs
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
//==================================================================================
public class Startup : MonoBehaviour
{
    public string NextBootScnName;

    #region 基底
    //==================================================================================
    // <summary>
    // 初期化
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // 何かあればここに
    }

    //==================================================================================
    // <summary>
    // 更新
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    void Update()
    {
        SceneManager.LoadScene(NextBootScnName);
    }
    #endregion
}
