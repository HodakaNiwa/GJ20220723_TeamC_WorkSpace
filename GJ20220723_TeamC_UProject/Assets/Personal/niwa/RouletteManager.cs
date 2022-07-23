using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==================================================================================
// <summary>
// ルーレット管理マネージャ
// </summary>
// <author> 丹羽 保貴(Niwa Hodaka)</author>
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


    #region インスペクタ公開
    public GameObject PlayerObjectRef = null;
    public float StartWaitTime = 1.0f;
    public float NextRouletteStartInterval = 1.0f;
    public float AllDecideWaitTime = 1.0f;
    public float RouletteTime = 1.0f;
    #endregion


    #region 非公開フィールド
    private PartsController _partsController = null;
    private PartsEntry _nowRouletteEntry = null;
    private int _nowRoulettePartsIndex = 0;
    private eFlow _nowFlow = eFlow.Start;
    private float _elapsedCounter = .0f;
    #endregion


    #region 基底
    //==================================================================================
    // <summary>
    // 初期化
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
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
    // 更新
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
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


    #region フロー処理
    //==================================================================================
    // <summary>
    // Flow ; 最初の待機
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_WaitStart()
    {
        // 時が来るまで待つ
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter < StartWaitTime)
        {
            return;
        }

        // 頭から開始
        if (_partsController.TryGetPartsEntry((ePARTS)_nowRoulettePartsIndex, out var entry))
        {
            OperableStateRoulette(entry, false);
        }

        // フロー切り替え
        ChangeFlow(eFlow.Roulette);
    }

    //==================================================================================
    // <summary>
    // Flow ; ルーレット中
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_Roulette()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 現在のパーツのルーレットを止める
            if (_partsController.TryGetPartsEntry((ePARTS)_nowRoulettePartsIndex, out var entry))
            {
                OperableStateRoulette(entry, true);
            }

            // ルーレットの止まる演出待ちへ
            ChangeFlow(eFlow.WaitRouletteStop);
        }
    }

    //==================================================================================
    // <summary>
    // Flow ; ルーレット停止待ち演出
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_WaitRouletteStop()
    {
        // ルーレット停止まで待つ
        if (_nowRouletteEntry.RouletteComp.IsRoulette)
        {
            return;
        }

        // インターバルを設ける
        ChangeFlow(eFlow.RouletteStopInterval);
    }

    //==================================================================================
    // <summary>
    // Flow ; ルーレットが停止した後のインターバル
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_RouletteStopInterval()
    {
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter >= NextRouletteStartInterval)
        {
            // 決定するパーツがもうないなら終了フローへ移行
            if (_nowRoulettePartsIndex >= (int)ePARTS.End)
            {
                ChangeFlow(eFlow.AllDecide);
                return;
            }

            // 次のルーレットを開始
            ++_nowRoulettePartsIndex;
            if (_partsController.TryGetPartsEntry((ePARTS)_nowRoulettePartsIndex, out var entry))
            {
                OperableStateRoulette(entry, false);
            }

            // 再びルーレットフローへ
            ChangeFlow(eFlow.Roulette);
        }
    }

    //==================================================================================
    // <summary>
    // Flow ; 全部決まった後のインターバル
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void FlowEvent_AllDecide()
    {
        _elapsedCounter += Time.deltaTime;
        if (_elapsedCounter >= AllDecideWaitTime)
        {
            // ゲーム画面に遷移
        }
    }
    #endregion


    #region 非公開メソッド
    //==================================================================================
    // <summary>
    // フローの切り替え処理
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void ChangeFlow(eFlow nextFlow)
    {
        _nowFlow = nextFlow;
        _elapsedCounter = .0f;
    }

    //==================================================================================
    // <summary>
    // ルーレットの状態を操作する
    // </summary>
    // <author> 丹羽 保貴(Niwa Hodaka)</author>
    //==================================================================================
    private void OperableStateRoulette(PartsEntry entry, bool isStop)
    {
        if (entry == null)
        {
            return;
        }

        // ルーレット操作
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
