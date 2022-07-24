using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultMove : MonoBehaviour
{

    public bool IsPlayerHit = false;
    public bool IsCreateResult = false;
    public float MoveTimer = 2.0f;
    public float MoveValue = .01f;
    public GameObject PlayerObject = null;
    public Vector3 Offset;
    public GameObject ResultPrefab;
    private float _elapsedCounter = .0f;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerHit && !IsCreateResult)
        {
            //MoveTrack();
            _elapsedCounter += Time.deltaTime;
            if(_elapsedCounter >= MoveTimer)
            {
                Instantiate(ResultPrefab);
                IsCreateResult = true;
                SceneManager.LoadScene("Result");
            }
        }
    }


    private void MoveTrack()
    {
        if (PlayerObject != null)
        {
            PlayerObject.transform.position = transform.position + Offset;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerObject = collision.gameObject;
            var playerComp = collision.gameObject.GetComponent<PlayerUpdater>();
            if (playerComp != null && ResidentVisualizeHolder.Instance != null)
            {
                ResidentVisualizeHolder.Instance.GameTimer = playerComp.GameTimer;
            }
            IsPlayerHit = true;
        }
    }
}
