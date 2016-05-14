using UnityEngine;
using System.Collections;
using System;

public class ScreenShake : MonoBehaviour {
    public static ScreenShake instance;
    void Awake()
    {
        instance = this;
    }

        CameraScript cam;
    float currentShakeTimeLeft = 0f;
    public float shakeAmount = 0.25f;
    public float maxShakeTime = 2f;
    float lastShakeTime = -9f;
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
    //Called by the drill
    public void Shake()
    {
        currentShakeTimeLeft = maxShakeTime;
    }

    private Vector3 GetShakeValue()
    {
        currentShakeTimeLeft -= Time.deltaTime;
        if (currentShakeTimeLeft < 0)
            currentShakeTimeLeft = 0;
        return new Vector3(currentShakeTimeLeft * shakeAmount * random1(), currentShakeTimeLeft* shakeAmount * random1(), 0);
    }

    private float random1()
    {
        var chance = UnityEngine.Random.Range(0, 100f);
        if (chance > 50f)
            return -1f;
        else
            return 1f;
    }
}
