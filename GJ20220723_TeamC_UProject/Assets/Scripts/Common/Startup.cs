using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//==================================================================================
// <summary>
// Startup.cs
// </summary>
// <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
//==================================================================================
public class Startup : MonoBehaviour
{
    #region Šî’ê
    //==================================================================================
    // <summary>
    // ‰Šú‰»
    // </summary>
    // <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
    //==================================================================================
    void Start()
    {
        // ‰½‚©‚ ‚ê‚Î‚±‚±‚É
    }

    //==================================================================================
    // <summary>
    // XV
    // </summary>
    // <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
    //==================================================================================
    void Update()
    {
        SceneManager.LoadScene("TestRouletteScn_niwa");
    }
    #endregion
}
