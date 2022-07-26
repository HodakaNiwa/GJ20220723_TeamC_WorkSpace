using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBonePosition : MonoBehaviour
{

    [SerializeField]
    private GameObject StandardBone = null;

   
    public void setStandardBone(Vector3 pos)
    {
        if (StandardBone == null)
        {
            Debug.LogWarning(gameObject.name + "基準Boneを設定してください");
            return;
        }

        StandardBone.transform.position = pos;
    }

}
