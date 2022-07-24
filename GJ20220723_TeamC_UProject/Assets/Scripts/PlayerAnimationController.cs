using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private PlayerUpdater PlayerUpdater;
    private Animator Animator = null;

    // Start is called before the first frame update
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();

        PlayerUpdater = gameObject.transform.parent.gameObject.GetComponent<PlayerUpdater>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Animator == null || PlayerUpdater == null)
        {
            return;
        }

        Animator.SetBool("IsJump", PlayerUpdater.IsJump);
        Animator.SetBool("IsMove", PlayerUpdater.IsMove);
        
    }
}
