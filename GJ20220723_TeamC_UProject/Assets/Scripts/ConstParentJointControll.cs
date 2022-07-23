using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstParentJointControll : MonoBehaviour
{

    [SerializeField]
    private GameObject HeadParts = null;
    [SerializeField]
    private GameObject HandRParts = null;
    [SerializeField]
    private GameObject HandLParts = null;
    [SerializeField]
    private GameObject LegRParts = null;
    [SerializeField]
    private GameObject LegLParts = null;
    [SerializeField]
    private GameObject BoosterParts = null;

    private PartsJointPositionSettings JointOssetSettings;

    // Start is called before the first frame update
    void Start()
    {
        JointOssetSettings = HeadParts.GetComponent<PartsJointPositionSettings>();

        HandRParts.transform.localPosition = JointOssetSettings.HandROffset;
        HandLParts.transform.localPosition = JointOssetSettings.HandLOffset;
        LegLParts.transform.localPosition = JointOssetSettings.LegLOffset;
        LegRParts.transform.localPosition = JointOssetSettings.LegROffset;
        BoosterParts.transform.localPosition = JointOssetSettings.BoosterOffset;



    }

    // Update is called once per frame
    void Update()
    {

        HandRParts.transform.localPosition = JointOssetSettings.HandROffset;
        HandLParts.transform.localPosition = JointOssetSettings.HandLOffset;
        LegLParts.transform.localPosition = JointOssetSettings.LegLOffset;
        LegRParts.transform.localPosition = JointOssetSettings.LegROffset;
        BoosterParts.transform.localPosition = JointOssetSettings.BoosterOffset;

    }
}
