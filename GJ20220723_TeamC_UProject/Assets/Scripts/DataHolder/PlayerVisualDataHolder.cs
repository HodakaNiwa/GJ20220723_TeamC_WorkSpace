using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualDataHolder : MonoBehaviour
{
    public int ManageId = 0;
    public GameObject[] PartsPrefab = new GameObject[0];

    // Start is called before the first frame update
    void Start()
    {
        ResidentVisualizeHolder.Instance.RegisterVisualData(PartsPrefab, ManageId);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
