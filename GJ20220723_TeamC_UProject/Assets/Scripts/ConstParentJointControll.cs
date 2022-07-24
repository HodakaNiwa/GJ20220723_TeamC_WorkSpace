using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstParentJointControll : MonoBehaviour
{

    public int BodyID = 0;
    public int RHandID = 0;
    public int LHandID = 0;
    public int RLegID = 0;
    public int LLegID = 0;
    public int BoosterID = 0;


    public GameObject[] BodyParts;
    public GameObject[] RHandParts;
    public GameObject[] LHandParts;
    public GameObject[] RLegParts;
    public GameObject[] LLegParts;
    public GameObject[] BoosterParts;

    private GameObject BodyPart = null;
    private GameObject HandRPart = null;
    private GameObject HandLPart = null;
    private GameObject LegRPart = null;
    private GameObject LegLPart = null;
    private GameObject BoosterPart = null;

    private PartsJointPositionSettings JointOssetSettings;

    // Start is called before the first frame update
    void Start()
    {
        createParts();

        JointOssetSettings = BodyPart.GetComponent<PartsJointPositionSettings>();

        BodyPart.transform.localPosition = JointOssetSettings.SelfOffset;
       HandRPart.transform.localPosition = JointOssetSettings.HandROffset;
       HandLPart.transform.localPosition = JointOssetSettings.HandLOffset;
       LegLPart.transform.localPosition = JointOssetSettings.LegLOffset;
       LegRPart.transform.localPosition = JointOssetSettings.LegROffset;
       BoosterPart.transform.localPosition = JointOssetSettings.BoosterOffset;


    }

    private void createParts()
    {

        BodyPart = GameObject.Instantiate(BodyParts[BodyID]);
        HandRPart = GameObject.Instantiate(RHandParts[RHandID]);
        HandLPart = GameObject.Instantiate(LHandParts[LHandID]);
        LegRPart = GameObject.Instantiate(RLegParts[RLegID]);
        LegLPart = GameObject.Instantiate(LLegParts[LLegID]);
        BoosterPart = GameObject.Instantiate(BoosterParts[BoosterID]);

        BodyPart.transform.SetParent(gameObject.transform);
        HandRPart.transform.SetParent(gameObject.transform);
        HandLPart.transform.SetParent(gameObject.transform);
        LegRPart.transform.SetParent(gameObject.transform);
        LegLPart.transform.SetParent(gameObject.transform);
        BoosterPart.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {

        BodyPart.transform.localPosition = JointOssetSettings.SelfOffset;
        HandRPart.transform.localPosition = JointOssetSettings.HandROffset;
        HandLPart.transform.localPosition = JointOssetSettings.HandLOffset;
        LegLPart.transform.localPosition = JointOssetSettings.LegLOffset;
        LegRPart.transform.localPosition = JointOssetSettings.LegROffset;
        BoosterPart.transform.localPosition = JointOssetSettings.BoosterOffset;

    }
}
