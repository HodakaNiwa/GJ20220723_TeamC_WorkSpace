using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFireDrawControl : MonoBehaviour
{
    private PlayerUpdater PlayerUpdater;
    public SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerUpdater = gameObject.transform.parent.gameObject.GetComponent<PlayerUpdater>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerUpdater == null || Renderer == null)
        {
            return;
        }

        Renderer.enabled = PlayerUpdater.IsJump;
    }
}
