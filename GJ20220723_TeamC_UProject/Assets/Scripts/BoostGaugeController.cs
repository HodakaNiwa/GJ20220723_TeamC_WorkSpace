using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGaugeController : MonoBehaviour
{

    private RectTransform RectTransform = null;
    public PlayerUpdater PlayerUpdater;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RectTransform == null || PlayerUpdater == null)
        {
            return;
        }

        var scaleY = PlayerUpdater.JumpSecond - PlayerUpdater.JumpTimer;
        RectTransform.localScale = new Vector3(1.0f, scaleY, 1.0f);
    }
}
