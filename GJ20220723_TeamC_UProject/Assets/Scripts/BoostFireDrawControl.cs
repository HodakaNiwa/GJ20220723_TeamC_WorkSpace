using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostFireDrawControl : MonoBehaviour
{
    public PlayerUpdater PlayerUpdater;
    public SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        
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
