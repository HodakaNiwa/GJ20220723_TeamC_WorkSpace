using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlasyerUpdater : MonoBehaviour
{

    public enum PlayerActionState
    {
        Idle, 
        Move,
        GroundBoost,
        Jump,
        AirBoost, 
        Fall,
    }

    /// <summary>
    /// Playerの現在のAction管理
    /// </summary>
    public PlayerActionState ActionState = PlayerActionState.Idle;

    public bool OnGround = false;

    private Rigidbody2D Rigidbody = null;

    #region Walk

    public float HorizontalSpeed = 0.0f;

    public float HorizontalAccel => _HorizontalAccel * Time.deltaTime;
    [SerializeField]
    private float _HorizontalAccel = 0.1f;
    public float MaxHorizontalSpeed => _MaxHorizontalSpeed;
    [SerializeField]
    private float _MaxHorizontalSpeed = 1.0f;

    #endregion Walk

    #region Jump

    /// <summary>
    /// Jump 継続時間 この時間スペースキーを押し続けているとブーストに移行します
    /// スペースキーを放していたら 着地に移行します
    /// </summary>
    public float JumpSecond => _JumpSecond;
    [SerializeField]
    private float _JumpSecond = 1.0f;

    private float JumpTimer { get; set; } = 0.0f;



    public float VerticalSpeed = 0.0f;

    public float JumpPower => _JumpPower * Time.deltaTime;
    [SerializeField]
    private float _JumpPower = 10.0f;

    public float GravityPower => _GravityPower * Time.deltaTime;
    [SerializeField]
    private float _GravityPower = 0.98f;


    public float MaxVeriticalSpeed => _MaxVeriticalSpeed;
    [SerializeField]
    private float _MaxVeriticalSpeed = 2.0f;

    #endregion Jump

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAction();

        updatePosition();
    }


    /// <summary>
    /// Playerの行動更新
    /// </summary>
    private void updateAction()
    {
        switch (ActionState)
        {
            case PlayerActionState.Idle:

                if (isAnyLeftKeyDown() || isAnyRightKeyDown())
                {
                    ActionState = PlayerActionState.Move;
                    break;
                }

                if (OnGround == false)
                {
                    ActionState = PlayerActionState.Fall;
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    JumpTimer = 0.0f;
                    VerticalSpeed = JumpPower;
                    ActionState = PlayerActionState.Jump;
                    break;
                }

                HorizontalSpeed = 0.0f;
                VerticalSpeed = 0.0f;

                break;
            case PlayerActionState.Move:

                if (!isAnyLeftKeyPress() && !isAnyRightKeyPress())
                {   // 入力なくなったのでMove終了
                    ActionState = PlayerActionState.Idle;
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    JumpTimer = 0.0f;
                    VerticalSpeed = JumpPower;
                    ActionState = PlayerActionState.Jump;
                    break;
                }

                var accel = HorizontalAccel;
                if (isAnyLeftKeyPress())
                {
                    accel *= -1;
                }
                HorizontalSpeed = HorizontalSpeed + accel;
                if (HorizontalSpeed >= MaxHorizontalSpeed)
                {
                    HorizontalSpeed = MaxHorizontalSpeed;
                }
                if (HorizontalSpeed <= -MaxHorizontalSpeed)
                {
                    HorizontalSpeed = -MaxHorizontalSpeed;
                }


                    break;
            case PlayerActionState.Jump:

                if (VerticalSpeed <= 0.0f)
                {
                    ActionState = PlayerActionState.Fall;
                }

                VerticalSpeed -= GravityPower;

                JumpTimer += Time.deltaTime;
               

                break;
            case PlayerActionState.AirBoost:
                break;
            case PlayerActionState.Fall:

                if (OnGround)
                {
                    ActionState = PlayerActionState.Idle;
                }


                VerticalSpeed -= GravityPower;

                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 座標位置の更新
    /// </summary>
    private void updatePosition()
    {
        var position = gameObject.transform.position;
        var nextPosition = position + new Vector3(HorizontalSpeed, VerticalSpeed, 0.0f);
        gameObject.transform.position = nextPosition;


    }

    private bool isAnyLeftKeyDown()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D);
    }

    private bool isAnyRightKeyDown()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A);
    }

    private bool isAnyLeftKeyPress()
    {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D);
    }

    private bool isAnyRightKeyPress()
    {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            OnGround = true;

            if (JumpPower <= 0.0f)
            {
            }
        }
    }


    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            OnGround = false;
        }
    }
}
