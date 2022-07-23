using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // ポーズメニュの要素
    [SerializeField] private GameObject _PausePanel;
    [SerializeField] private Button _ResumeButton;
    [SerializeField] private Button _StageSelectButton;

    void Start()
    {
        _PausePanel.SetActive(false);
        _ResumeButton.onClick.AddListener(resumeGame);
        _StageSelectButton.onClick.AddListener(goToSelectStage);
    }

    void Update() 
    {
        // キー入力の監視
        getKeyInput();
    }

    // ポーズ時の挙動
    private void pauseGame()
    {
        // 時間停止
        Time.timeScale = 0;
        _PausePanel.SetActive(true);
    }

    // ポーズ終了の挙動
    private void resumeGame()
    {
        // 時間停止終了
        Time.timeScale = 1;
        _PausePanel.SetActive(false);
    }   

    // ステージ画面への遷移ボタンの挙動
    private void goToSelectStage()
    {
        // 時間停止終了
        Time.timeScale = 1;
        _PausePanel.SetActive(false);
    }  

    // キー入力の取得
    private void getKeyInput()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            pauseGame();
        }        
    }
}
