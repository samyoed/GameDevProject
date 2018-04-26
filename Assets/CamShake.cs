

using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{

    Vector3 originalCameraPosition = new Vector3(2024.25f, 62.1105f, -10f);

    float shakeAmt = 0;

    public Camera mainCamera;

    void OnCollisionEnter2D(Collision2D coll)
    {

        shakeAmt = coll.relativeVelocity.magnitude * .025f;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }

}
