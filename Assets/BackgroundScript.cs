using UnityEngine;
using System.Collections.Generic;
using System;

public class BackgroundScript : MonoBehaviour {

    List<Transform> backgrounds = new List<Transform>();
    List<Vector3> backgroundStartPositions = new List<Vector3>();

    void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            backgrounds.Add(gameObject.transform.GetChild(i));
        }

        for (int b = 0; b < backgrounds.Count; b++)
        {
            backgroundStartPositions.Add(backgrounds[b].position);
        }
    }

	// Update is called once per frame
	void LateUpdate () {
        if (CameraScript.instance.viewState == CameraScript.state.MapView)
            BackgroundMapView();
        else if (CameraScript.instance.viewState == CameraScript.state.LevelView)
            BackgroundLevelView();
	}

    private void BackgroundLevelView()
    {
        for (int b = 0; b < backgrounds.Count; b++)
        {
            backgrounds[b].position = new Vector3(backgroundStartPositions[b].x + ( backgrounds[b].position.z * (PlayerController.instance.transform.position.x -1.9f)), backgrounds[b].position.y, backgrounds[b].position.z);
        }
    }

    private void BackgroundMapView()
    {
        for (int b = 0; b < backgrounds.Count; b++)
        {
            backgrounds[b].position = backgroundStartPositions[b];
        }
    }
}
