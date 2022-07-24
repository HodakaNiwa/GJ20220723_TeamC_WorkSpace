using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOctagon : MonoBehaviour
{
    // ‰ñ“]‚Ì‘¬‚³
    [SerializeField]
    private float _RotateSpeed = 0.1f;

    void Update()
    {
        rotateOctagon();
    }


    /// <summary>
    /// ”ªŠpŒ`‚ğ‰ñ“]‚³‚¹‚éŠÖ”
    /// </summary>
    private void rotateOctagon()
    {
        this.transform.Rotate(0,0, _RotateSpeed * Time.deltaTime, Space.Self);
    }
}
