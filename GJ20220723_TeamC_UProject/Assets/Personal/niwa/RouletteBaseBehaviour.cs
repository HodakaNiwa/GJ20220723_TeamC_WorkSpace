//==================================================================================
// <summary>
// RouletteBaseBehaviour.cs
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// ���[���b�g�������
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class RouletteBaseBehaviour : MonoBehaviour
{
    #region ���J�v���p�e�B
    /// <summary> �I�����ꂽ�ԍ� </summary>
    public int SelectedIdx => (int)_targetList[_selectedIdx];

    /// <summary> ���݃��[���b�g���� </summary>
    public bool IsRoulette => _isRoulette;
    #endregion


    #region ���J�t�B�[���h
    /// <summary> ���[���b�g����]����b�� </summary>
    [SerializeField]
    protected float _rouletteRollTime = .0f;

    /// <summary> ���[���b�g����]�����邩 </summary>
    [SerializeField]
    protected bool _isRoulette = false;
    #endregion


    #region ����J�t�B�[���h
    /// <summary> �I�����ꂽ�ԍ� </summary>
    [SerializeField]
    private int _selectedIdx = 0;

    /// <summary> ���[���b�g�ΏۂƂȂ�ԍ� </summary>
    [SerializeField]
    private List<int> _targetList = new List<int>();

    /// <summary> ���Ԍv���p�J�E���^�[ </summary>
    private float _elapsedCounter = .0f;

    /// <summary> �p�[�c��VM </summary>
    private PartsVMBaseBehaviour _partsVMComp = null;
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
        if (!Setup())
        {
            Debug.Log("�Z�b�g�A�b�v���s�I");
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
        // ��]�t���O���܂�Ă����珈�����Ȃ�
        if (!_isRoulette)
        {
            return;
        }

        // �J�E���^�[�����Z���A���l�ɒB�����烉���_���őI��ԍ��ݒ�
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter >= _rouletteRollTime)
        {
            _elapsedCounter = .0f;
            _selectedIdx = (_selectedIdx + 1) % _targetList.Count;
            _partsVMComp.UpdateVM(_selectedIdx);
        }
    }
    #endregion


    #region �p���z�胁�\�b�h
    //==================================================================================
    // <summary>
    // �ݒ�t�@�C���ǂݍ���
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public virtual bool LoadRouletteConfig() { return true; }
    #endregion


    #region ���J���\�b�h
    //==================================================================================
    // <summary>
    // ���[���b�g�J�n
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public bool StartRoulette()
    {
        // �ΏۂƂȂ�ԍ����X�g��ݒ�
        _targetList.Clear();

        // �Ώۂ�0�̏ꍇ�͏������Ȃ�
        if (_targetList.Count <= 0)
        {
            Debug.Log("�Ώۂ��Ȃ����߁A���[���b�g�J�n���X�L�b�v���܂�");
            return false;
        }

        // �t���OON
        _isRoulette = true;

        return true;
    }

    //==================================================================================
    // <summary>
    // ���[���b�g�I��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public void EndRoulette()
    {
        // �t���OOFF
        _isRoulette = false;

        // �J�E���^�[�����Z�b�g
        _elapsedCounter = .0f;

        Debug.Log("���[���b�g�X�g�b�v");
    }
    #endregion


    #region ����J���\�b�h
    //==================================================================================
    // <summary>
    // �Z�b�g�A�b�v
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private bool Setup()
    {
        // �O�̂��߃��X�g��������
        _targetList.Clear();

        // �O���t�@�C������ݒ�ǂݍ���
        if (!LoadRouletteConfig())
        {
            Debug.Log("�ݒ�ǂݍ��ݎ��s");
            return false;
        }

        // �e��R���|�[�l���g���擾
        _partsVMComp = GetComponent<PartsVMBaseBehaviour>();
        if (_partsVMComp != null)
        {
            for (var i = 0; i < _partsVMComp.SourceTexNum; ++i)
            {
                _targetList.Add(i);
            }
        }

        return true;
    }
    #endregion
}
