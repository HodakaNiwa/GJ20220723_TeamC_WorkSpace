using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// ƒf[ƒ^ƒzƒ‹ƒ_[
// </summary>
// <author> ’O‰H •Û‹M(Niwa Hodaka)</author>
//==================================================================================
public abstract class DataHolderBase : MonoBehaviour
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
        RegisterData();
        gameObject.SetActive(false);
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


    protected abstract void RegisterData();
}
