using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUpdater : MonoBehaviour
{

    public enum PlayerActionState
    {
        Idle, 
        Move,
        Jump,
        Fall,
    }

    /// <summary>
    /// Playerの現在のAction管理
    /// </summary>
    public PlayerActionState ActionState = PlayerActionState.Idle;

    public bool IsMove => ActionState == PlayerActionState.Move;
    public bool IsJump => ActionState == PlayerActionState.Jump;

    public bool OnGround = false;

    public bool IsLookRight = false;

    private Rigidbody2D Rigidbody = null;

    #region Walk

    public float HorizontalSpeed = 0.0f;

    public float MovePower => _MovePower * Time.deltaTime;
    [SerializeField]
    private float _MovePower = 0.1f;
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

    public float JumpTimer { get; private set; } = 0.0f;

    public float VerticalSpeed = 0.0f;

    public float JumpPower => _JumpPower * Time.deltaTime;
    [SerializeField]
    private float _JumpPower = 10.0f;

    public float MovePowerInJump => _MovePowerInJump * Time.deltaTime;
    [SerializeField]
    private float _MovePowerInJump = 5.0f;

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
                    ActionState = PlayerActionState.Jump;
                    break;
                }

                var accel = MovePower;
                if (isAnyLeftKeyPress())
                {
                    IsLookRight = false;
                    accel *= -1;
                }
                else
                {
                    IsLookRight = true;
                }

                HorizontalSpeed = accel;

                    break;

            case PlayerActionState.Jump:

                JumpTimer += Time.deltaTime;
                if (JumpTimer > JumpSecond)
                {
                    ActionState = PlayerActionState.Fall;
                    break;
                }

                if (!Input.GetKey(KeyCode.Space))
                {
                    ActionState = PlayerActionState.Fall;
                    break;
                }

                VerticalSpeed = JumpPower;

                var horizAccel = MovePowerInJump;
                if (isAnyLeftKeyPress())
                {
                    IsLookRight = false;
                    horizAccel *= -1;
                }
                else if (isAnyRightKeyPress())
                {
                    IsLookRight = true;
                }
                else 
                {
                    break;
                }

                HorizontalSpeed = HorizontalSpeed + horizAccel;
                if (HorizontalSpeed > MaxHorizontalSpeed)
                {
                    HorizontalSpeed = MaxHorizontalSpeed;
                }
                else if (HorizontalSpeed < -MaxHorizontalSpeed)
                {
                    HorizontalSpeed = -MaxHorizontalSpeed;
                }

                break;
          
            case PlayerActionState.Fall:

                if (OnGround)
                {
                    JumpTimer = 0.0f;
                    ActionState = PlayerActionState.Idle;
                    break;
                }

                if (Input.GetKeyDown(KeyCode.Space) && JumpTimer < JumpSecond)
                {
                    ActionState = PlayerActionState.Jump;
                    break;
                }

                VerticalSpeed -= GravityPower;
                if (VerticalSpeed < -MaxVeriticalSpeed)
                {
                    VerticalSpeed = -MaxVeriticalSpeed;
                }


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
        if (VerticalSpeed > JumpPower)
        {   // ジャンプ以上のスピードで上に行く必要はないはず
            VerticalSpeed = JumpPower;
        }

        var position = gameObject.transform.position;
        var nextPosition = position + new Vector3(HorizontalSpeed, VerticalSpeed, 0.0f);
        gameObject.transform.position = nextPosition;

        gameObject.transform.rotation = Quaternion.identity;

        var scaleX = IsLookRight ? 1 : -1;
        gameObject.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
    }

    #region InputCheck

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

    private bool isShiftKeyDown()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    }

    private bool isShiftKeyPress()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    #endregion InputCheck



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
