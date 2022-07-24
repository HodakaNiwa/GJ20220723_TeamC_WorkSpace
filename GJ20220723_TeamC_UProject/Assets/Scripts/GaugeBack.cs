using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBack : MonoBehaviour
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

        var scaleY = PlayerUpdater.JumpSecond;
        RectTransform.localScale = new Vector3(scaleY, 1.0f, 1.0f);
    }
}
