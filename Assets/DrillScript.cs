using UnityEngine;
using System.Collections;
using System;

public class DrillScript : MonoBehaviour {

    float lastStart;
    enum states { offline, movingForwards, drilling, movingBackwards}
    states state = states.offline;

    Vector2 targetPos;
    Vector2 startPos;
    float currentTimeDriving;
    public float offlineCD;
    public float driveForwardTotalTime = 2f;
    public float driveDrillTotalTime = 2f;
    public float driveBackwardTotalTime = 2f;

    public float driveDistance = 1f;

    public int particleAmounts = 10;

    bool hasDrilled = false;

	// Update is called once per frame
	void Update () {
        if (Time.time > (lastStart + offlineCD) && state == states.offline)
        {
            startPos = transform.position;
            MoveForward();
        }
        else if(state == states.drilling)
        {
            Drilling();
        }
        else if (state == states.movingForwards)
        {
            targetPos = startPos + new Vector2(-driveDistance, 0);
            MoveForward();
        }
        else if (state == states.movingBackwards)
            MoveBackwards();
    }

    private void Drilling()
    {
        state = states.drilling;
        currentTimeDriving += Time.deltaTime;

        if (currentTimeDriving > driveDrillTotalTime)
        {
            currentTimeDriving = 0f;
            startPos = transform.position;
            targetPos = startPos + new Vector2(driveDistance, 0);
            MoveBackwards();
        }
        else if(currentTimeDriving > (driveDrillTotalTime * 0.5f))
        {
            hasDrilled = true;
            DrillShake();
        }    
    }

    private void MoveBackwards()
    {
        state = states.movingBackwards;
        currentTimeDriving += Time.deltaTime;

        if (currentTimeDriving > driveBackwardTotalTime)
        {
            currentTimeDriving = 0f;
            GoOffline();
        }
        else
        {
            transform.position = Vector2.Lerp(startPos, targetPos, currentTimeDriving / driveBackwardTotalTime);
        }
    }

    private void MoveForward()
    {
        state = states.movingForwards;
        currentTimeDriving += Time.deltaTime;

        if (currentTimeDriving > driveForwardTotalTime)
        {
            currentTimeDriving = 0f;
            Drilling();
        }
        else
        {
            transform.position = Vector2.Lerp(startPos, targetPos, currentTimeDriving / driveForwardTotalTime);
        }
    }

    void DrillShake()
    {
        ParticleSystemAdmin.instance.Emit(particleAmounts,particleSystemType.LeftWall);
        ScreenShake.instance.Shake();
    }

    void GoOffline()
    {
        hasDrilled = false;
        state = states.offline;
        lastStart = Time.time;
    }
}
