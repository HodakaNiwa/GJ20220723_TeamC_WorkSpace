using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsJointPositionSettings : MonoBehaviour
{
    public Vector3 HandROffset => _HandROffset;
    [SerializeField]
    private Vector3 _HandROffset = new Vector3();

    public Vector3 HandLOffset => _HandLOffset;
    [SerializeField]
    private Vector3 _HandLOffset = new Vector3();

    public Vector3 LegROffset => _LegROffset;
    [SerializeField]
    private Vector3 _LegROffset = new Vector3();

    public Vector3 LegLOffset => _LegLOffset;
    [SerializeField]
    private Vector3 _LegLOffset = new Vector3();
    public Vector3 BoosterOffset => _BoosterOffset;
    [SerializeField]
    private Vector3 _BoosterOffset = new Vector3();

}
