using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreCalc : MonoBehaviour
{
    private Text _text = null;

    [SerializeField]
    private int MaxValue = 10000;

    [SerializeField]
    private int SubValue = 1;

    [SerializeField]
    private float Timer = .0f;


    // Start is called before the first frame update
    void Start()
    {
        if (ResidentVisualizeHolder.Instance != null)
        {
            Timer = ResidentVisualizeHolder.Instance.GameTimer;
        }

        // タイマーを使う
        var timer_int = (int)Timer;
        var value = MaxValue - (SubValue * timer_int);
        _text = gameObject.GetComponent<Text>();
        if (_text != null)
        {
            _text.text = value.ToString();
            Debug.Log(value);
        }
        else
        {
            Debug.Log("a");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
