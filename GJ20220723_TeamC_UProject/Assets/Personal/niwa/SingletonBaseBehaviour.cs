using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SingletonBaseBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Type t = typeof(T);

                _instance = (T)FindObjectOfType(t);
                if (_instance == null)
                {
                    Debug.LogError(t + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (_instance == null)
        {
            _instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}
