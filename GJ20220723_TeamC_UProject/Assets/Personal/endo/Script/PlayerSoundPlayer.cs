using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundPlayer : MonoBehaviour
{
    // 保持しているオーディオ
    public AudioClip Idle;
    public AudioClip Move;
    public AudioClip Jump;
    public AudioClip Fall;
    public AudioClip Landing;
    private AudioSource _AudioSource;
    private PlayerUpdater _PlayerUpdater;
    private PlayerUpdater.PlayerActionState _State;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();
        _PlayerUpdater = gameObject.GetComponent<PlayerUpdater>();
        _State = gameObject.GetComponent<PlayerUpdater>().ActionState;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_PlayerUpdater.ActionState)
        {
            case PlayerUpdater.PlayerActionState.Idle:
                //_AudioSource.Stop();
                //_AudioSource.clip = Idle;
                //_AudioSource.Play();
                break;

            case PlayerUpdater.PlayerActionState.Move:
                //_AudioSource.Stop();
                //_AudioSource.clip = Move;
                //_AudioSource.Play();
                break;
                
            case PlayerUpdater.PlayerActionState.Jump:
                _AudioSource.clip = Jump;
                _AudioSource.Play();

                //if(_PlayerUpdater.i)

                break;

            case PlayerUpdater.PlayerActionState.Fall:
                //_AudioSource.Stop();
                //_AudioSource.clip = Fall;
                //_AudioSource.Play();
                //_AudioSource.PlayOneShot(this.Fall);

                if (_PlayerUpdater.OnGround)
                {
                    _AudioSource.clip = Landing;
                    _AudioSource.Play();
                    //_AudioSource.PlayOneShot(this.Landing);
                }
                break;

        }
        
    }
}
