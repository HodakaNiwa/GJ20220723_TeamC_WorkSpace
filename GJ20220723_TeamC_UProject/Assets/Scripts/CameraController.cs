using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform PlayerTransform;
    [SerializeField]
    private Vector3 Offset = new Vector3(0.0f, 3.0f, 0.0f);

    //[SerializeField]
    //private float CameraSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var y = -0.6f;
        var x = PlayerTransform.position.x;
        gameObject.transform.position = new Vector3(x, y, -10f);//, CameraSpeed);
    }
}
