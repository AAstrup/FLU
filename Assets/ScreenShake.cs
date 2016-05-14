using UnityEngine;
using System.Collections;
using System;

public class ScreenShake : MonoBehaviour {

    CameraScript cam;
    Vector3 cameraShake = new Vector3(0,0,0);
    float currentShakeValue = 0f;
    float maxShake = 2f;
	// Use this for initialization
	void Start () {
        cam = transform.GetComponent<CameraScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 finalPos;

        finalPos = cam.CustomUpdate();
        finalPos += GetShakeValue();
        transform.position = finalPos;
    }

    private Vector3 GetShakeValue()
    {
        throw new NotImplementedException();
    }
}
