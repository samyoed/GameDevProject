using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public GameObject player;
    public float smoothSpeed = 0.5f;
    public float leftOffset = -30;

    private Vector3 offset = new Vector3(54.6f, 41.19f, 0);
    private bool isFacingRight;
    public float cameraBack = -10;
    
	bool lockPos = false;

    // Use this for initialization
    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        isFacingRight = player.gameObject.GetComponent<BluePlayerBehavior>().isFacingRight;

		lockPos = player.GetComponent<BluePlayerBehavior> ().bossFightStart;
    }

    void FixedUpdate()
	{
		if (!lockPos) {
			Vector2 desiredPosition = player.transform.position + offset;
        

			if (!isFacingRight) {
				desiredPosition = new Vector3 (desiredPosition.x + leftOffset, desiredPosition.y, -1);

				Vector2 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

				transform.position = smoothedPosition;
			} else {
				Vector2 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

				transform.position = smoothedPosition;
			}

			transform.position = new Vector3 (transform.position.x, transform.position.y, cameraBack);

		}
	}
}
