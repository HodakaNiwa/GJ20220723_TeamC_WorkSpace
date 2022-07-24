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
    public enum eMOVEDIRECTION
    {
        Invalid = -1,

        Vertical,
        Horizontal,

        Max,

        Start = Vertical,
        End = Max - 1,
    }


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

    /// <summary> ���[���b�g����]���� </summary>
    [SerializeField]
    protected bool _isRoulette = false;

    /// <summary> �p�[�c���m�̃I�t�Z�b�g </summary>
    [SerializeField]
    private float _rouletteOffset = .0f;

    /// <summary> ���[���b�g�̉�]���� </summary>
    [SerializeField]
    private eMOVEDIRECTION _rouletteMoveDirection = eMOVEDIRECTION.Vertical;

    /// <summary> ���[���b�g�̏I�����N�G�X�g������Ă��邩 </summary>
    private bool _isStopRouletteRequest = false;
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

    /// <summary> �p�[�c�̃��[���b�g�R���g���[���[ </summary>
    private PartsRouletteController _partsRouletteController = null;

    private int _targetPartsIndex = 0;
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
        var moveValue = (_rouletteOffset / _rouletteRollTime) * Time.deltaTime;
        _partsRouletteController.MovePartsAll(_rouletteMoveDirection, moveValue);
        if (_elapsedCounter >= _rouletteRollTime)
        {
            _elapsedCounter = .0f;
            if (_isStopRouletteRequest)
            {
                StopRoulette();
                return;
            }
            else
            {
                _partsRouletteController.SwapPartsPosition(_rouletteMoveDirection, _rouletteOffset);
            }
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
    public bool StartRoulette(int layer)
    {
        // �Ώۂ�0�̏ꍇ�͏������Ȃ�
        if (_targetList.Count <= 0)
        {
            Debug.Log("�Ώۂ��Ȃ����߁A���[���b�g�J�n���X�L�b�v���܂�");
            return false;
        }

        // �t���OON
        _isRoulette = true;
        _partsRouletteController.ChangeVisibleExceptingSprite(true);
        _partsRouletteController.ChangeSpliteOrderInLayer(layer);

        return true;
    }

    //==================================================================================
    // <summary>
    // ���[���b�g�I��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public void RequestStopRoulette()
    {
        // �t���OOFF
        _isStopRouletteRequest = true;

        // �^�[�Q�b�g�p�[�c�̔ԍ����擾
        var currentVMIndex = _partsRouletteController.NextSwapPrefabIndex;
        for (var i = 0; i < 2; ++i)
        {
            currentVMIndex = currentVMIndex - 1;
            if (currentVMIndex < 0)
            {
                currentVMIndex = _partsRouletteController.SourcePrefabNum - 1;
            }
        }
        _targetPartsIndex = currentVMIndex;
        var entry = _partsRouletteController.GetRouletteEntry(currentVMIndex);
        if (entry != null)
        {
            _selectedIdx = entry.BindedPrefabIndex;
        }
    }

    //==================================================================================
    // <summary>
    // ���[���b�g�̉�]�X�s�[�h�ݒ�
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public void SetRouletteRollSpeed(float speed)
    {
        _rouletteRollTime = speed;
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
        // �e��R���|�[�l���g���擾
        _partsRouletteController = GetComponent<PartsRouletteController>();
        if (_partsRouletteController != null)
        {
            // �p�[�c��Instanciate����
            _partsRouletteController.InstanciateRouletteParts(_targetList);

            // ���[���b�g�̃p�[�c�f�ރI�t�Z�b�g��ݒ�
            _partsRouletteController.SetRoulettePartsOffset(_rouletteMoveDirection, _rouletteOffset);
        }

        return true;
    }

    //==================================================================================
    // <summary>
    // ���[���b�g��~
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    public void StopRoulette()
    {
        // �t���OOFF
        _isRoulette = false;
        _isStopRouletteRequest = false;
        _partsRouletteController.ChangeVisibleExceptingSprite(false);
        _partsRouletteController.ResetSpliteOrderInLayer();
        _partsRouletteController.SwapCurrentPartsResource(_selectedIdx);

        // �J�E���^�[�����Z�b�g
        _elapsedCounter = .0f;
    }
    #endregion
}
