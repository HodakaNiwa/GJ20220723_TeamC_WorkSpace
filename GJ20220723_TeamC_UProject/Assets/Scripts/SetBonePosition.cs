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
            Debug.LogWarning(gameObject.name + "Šî€Bone‚ğİ’è‚µ‚Ä‚­‚¾‚³‚¢");
            return;
        }

        StandardBone.transform.position = pos;
    }

}
