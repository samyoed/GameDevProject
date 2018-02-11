using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public GameObject player;
    public float smoothSpeed = 0.5f;
    public float leftOffset = -30;

    private Vector3 offset;
    private bool isFacingRight;
    


    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        isFacingRight = player.gameObject.GetComponent<BluePlayerBehavior>().isFacingRight;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        

        if (!isFacingRight)
        {
            desiredPosition = new Vector3(desiredPosition.x + leftOffset, desiredPosition.y);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition , smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
        else
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }

    }
}
