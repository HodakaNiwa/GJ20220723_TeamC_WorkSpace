using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSprite : MonoBehaviour
{
    #region ���J�v���p�e�B
    public int DefaultOrderInLayer => _defaultOrderInLayer;

    public SpriteMaskInteraction MaskInteraction
    {
        get
        {
            return _renderer.maskInteraction;
        }
        set
        {
            _renderer.maskInteraction = value;
        }
    }
    #endregion


    #region ����J�t�B�[���h
    private SpriteRenderer _renderer = null;
    private int _defaultOrderInLayer = 0;
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
    public void Init()
    {
        // �e��R���|�[�l���g�擾
        _renderer = gameObject.GetComponent<SpriteRenderer>();

        if (_renderer != null)
        {
            // �f�t�H���g�̕`�惌�C���[�ԍ���ۑ�
            _defaultOrderInLayer = _renderer.sortingOrder;
        }
    }

    public void SetOrderInLayer(int layer)
    {
        _renderer.sortingOrder = layer;
    }

    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
    }
    #endregion
}
