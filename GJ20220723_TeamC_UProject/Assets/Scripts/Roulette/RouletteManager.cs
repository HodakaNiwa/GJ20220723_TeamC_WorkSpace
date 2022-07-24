using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// ���[���b�g�Ǘ��}�l�[�W��
// </summary>
// <author> �O�H �ۋM(Niwa Hodaka)</author>
//==================================================================================
public class RouletteManager : MonoBehaviour
{
    private enum eFlow
    {
        Invalid = -1, 

        WaitStart,
        Roulette,
        WaitRouletteStop,
        RouletteStopInterval,
        AllDecide,

        Max,
        Start = WaitStart,
        End = Max - 1,
    }


    #region �C���X�y�N�^���J
    public GameObject PlayerObjectRef = null;
    public float StartWaitTime = 1.0f;
    public float NextRouletteStartInterval = 1.0f;
    public float AllDecideWaitTime = 1.0f;
    public float RouletteTime = 1.0f;
    #endregion


    #region ����J�t�B�[���h
    private PartsController _partsController = null;
    private PartsEntry _nowRouletteEntry = null;
    private int _nowRoulettePartsIndex = 0;
    private eFlow _nowFlow = eFlow.Start;
    private float _elapsedCounter = .0f;
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
        if (PlayerObjectRef != null)
        {
            _partsController = PlayerObjectRef.GetComponent<PartsController>();
            _partsController.SetRouletteSppedOnAllPartsEntry(RouletteTime);
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
        switch (_nowFlow)
        {
            case eFlow.WaitStart:
                FlowEvent_WaitStart();
                break;

            case eFlow.Roulette:
                FlowEvent_Roulette();
                break;

            case eFlow.WaitRouletteStop:
                FlowEvent_WaitRouletteStop();
                break;

            case eFlow.RouletteStopInterval:
                FlowEvent_RouletteStopInterval();
                break;

            case eFlow.AllDecide:
                FlowEvent_AllDecide();
                break;
        }
    }
    #endregion


    #region �t���[����
    //==================================================================================
    // <summary>
    // Flow ; �ŏ��̑ҋ@
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_WaitStart()
    {
        // ��������܂ő҂�
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter < StartWaitTime)
        {
            return;
        }

        // ������J�n
        if (_partsController.TryGetPartsEntry((ePARTS)_nowRoulettePartsIndex, out var entry))
        {
            OperableStateRoulette(entry, false);
        }

        // �t���[�؂�ւ�
        ChangeFlow(eFlow.Roulette);
    }

    //==================================================================================
    // <summary>
    // Flow ; ���[���b�g��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_Roulette()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ���݂̃p�[�c�̃��[���b�g���~�߂�
            if (_partsController.TryGetPartsEntry((ePARTS)_nowRoulettePartsIndex, out var entry))
            {
                OperableStateRoulette(entry, true);
            }

            // ���[���b�g�̎~�܂鉉�o�҂���
            ChangeFlow(eFlow.WaitRouletteStop);
        }
    }

    //==================================================================================
    // <summary>
    // Flow ; ���[���b�g��~�҂����o
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_WaitRouletteStop()
    {
        // ���[���b�g��~�܂ő҂�
        if (_nowRouletteEntry.RouletteComp.IsRoulette)
        {
            return;
        }

        // �C���^�[�o����݂���
        ChangeFlow(eFlow.RouletteStopInterval);
    }

    //==================================================================================
    // <summary>
    // Flow ; ���[���b�g����~������̃C���^�[�o��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_RouletteStopInterval()
    {
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter >= NextRouletteStartInterval)
        {
            // ���肷��p�[�c�������Ȃ��Ȃ�I���t���[�ֈڍs
            if (_nowRoulettePartsIndex >= (int)ePARTS.End)
            {
                ChangeFlow(eFlow.AllDecide);
                return;
            }

            // ���̃��[���b�g���J�n
            ++_nowRoulettePartsIndex;
            if (_partsController.TryGetPartsEntry((ePARTS)_nowRoulettePartsIndex, out var entry))
            {
                OperableStateRoulette(entry, false);
            }

            // �Ăу��[���b�g�t���[��
            ChangeFlow(eFlow.Roulette);
        }
    }

    //==================================================================================
    // <summary>
    // Flow ; �S�����܂�����̃C���^�[�o��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_AllDecide()
    {
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter >= AllDecideWaitTime)
        {
            // �Q�[����ʂɑJ��
        }
    }
    #endregion


    #region ����J���\�b�h
    //==================================================================================
    // <summary>
    // �t���[�̐؂�ւ�����
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void ChangeFlow(eFlow nextFlow)
    {
        _nowFlow = nextFlow;
        _elapsedCounter = .0f;
    }

    //==================================================================================
    // <summary>
    // ���[���b�g�̏�Ԃ𑀍삷��
    // </summary>
    // <author> �O�H �ۋM(Niwa Hodaka)</author>
    //==================================================================================
    private void OperableStateRoulette(PartsEntry entry, bool isStop)
    {
        if (entry == null)
        {
            return;
        }

        // ���[���b�g����
        if (!isStop)
        {
            entry.RouletteComp.StartRoulette((int)ePARTS.Max);
            _nowRouletteEntry = entry;
        }
        else
        {
            entry.RouletteComp.RequestStopRoulette();
        }
    }
    #endregion
}
