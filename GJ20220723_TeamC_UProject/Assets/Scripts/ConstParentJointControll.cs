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
        if (ResidentVisualizeHolder.Instance != null)
        {
            BodyID = ResidentVisualizeHolder.Instance.GetPartsVisualDataIndex(ePARTS.Head_Body);
            RHandID = ResidentVisualizeHolder.Instance.GetPartsVisualDataIndex(ePARTS.Hand_R);
            LHandID = ResidentVisualizeHolder.Instance.GetPartsVisualDataIndex(ePARTS.Hand_L);
            RLegID = ResidentVisualizeHolder.Instance.GetPartsVisualDataIndex(ePARTS.Leg_R);
            LLegID = ResidentVisualizeHolder.Instance.GetPartsVisualDataIndex(ePARTS.Leg_L);
            BoosterID = ResidentVisualizeHolder.Instance.GetPartsVisualDataIndex(ePARTS.Ex_Boost);

            Debug.Log(BodyID);
            Debug.Log(RHandID);
            Debug.Log(LHandID);
            Debug.Log(RLegID);
            Debug.Log(LLegID);
            Debug.Log(BoosterID);
        }

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

        var boostParam = BoosterPart.GetComponent<BoosterParam>();
        var playerUpdater = GetComponent<PlayerUpdater>();
        if (boostParam != null && playerUpdater != null)
        {
            playerUpdater.JumpPower = boostParam.BoostPower;
            playerUpdater.JumpSecond = boostParam.BoostSec;
        }
        
        if (playerUpdater != null)
        {
            if (RHandID == LHandID)
            {
                playerUpdater.JumpPartsRate = 0.0f;
                playerUpdater.JumpPartPowerRate = 1.0f;
            }else if (RHandID == 2)
            {   // 1ç∑Ç™Ç†ÇÈ
                if (LHandID == 0)
                {   // âEÇ™ëÂÇ´Ç¢
                    playerUpdater.JumpPartsRate = 1.0f;
                    playerUpdater.JumpPartPowerRate = 1.0f;
                }
                else
                {   // ç∂Ç™ëÂÇ´Ç¢
                    playerUpdater.JumpPartsRate = -1.0f;
                    playerUpdater.JumpPartPowerRate = 1.0f;
                }
            }
            else if (RHandID == 0)
            {
                if (LHandID == 2)
                {   // ç∂Ç™ëÂÇ´Ç¢ ç∑Ç™1
                    playerUpdater.JumpPartsRate = -1.0f;
                    playerUpdater.JumpPartPowerRate = 1.0f;
                }
                else
                {   // ç∂Ç™ëÂÇ´Ç¢ ç∑Ç™2
                    playerUpdater.JumpPartsRate = -1.2f;
                    playerUpdater.JumpPartPowerRate = 1.2f;
                }
            }else if (RHandID == 1)
            {
                if (LHandID == 2)
                {   // âEÇ™ëÂÇ´Ç¢ ç∑Ç™1
                    playerUpdater.JumpPartsRate = 1.0f;
                    playerUpdater.JumpPartPowerRate = 1.0f;
                }
                else
                {   // âEÇ™ëÂÇ´Ç¢ ç∑Ç™2
                    playerUpdater.JumpPartsRate = 1.2f;
                    playerUpdater.JumpPartPowerRate = 1.2f;
                }
            }

        }

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
