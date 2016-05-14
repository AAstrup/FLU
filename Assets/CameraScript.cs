using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    void Awake() { instance = this; }

    [SerializeField]
    protected string inputKey = "e";
    [SerializeField]
    public state viewState = state.LevelView;
    public enum state { LevelView, MapView }
    Dictionary<levelYPos, float> levelToCameraYPos = new Dictionary<levelYPos, float>();
    Camera cam;
    List<KeyValuePair<float, levelYPos>> LineYPos = new List<KeyValuePair<float, levelYPos>>();
    levelYPos lastLevelPlayerWasAt = levelYPos.Bot;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        levelToCameraYPos.Add(levelYPos.Bot, -3.45f);
        LineYPos.Add(new KeyValuePair<float, levelYPos>(-3.45f, levelYPos.Bot));
        levelToCameraYPos.Add(levelYPos.Mid, -0.15f);
        LineYPos.Add(new KeyValuePair<float, levelYPos>(-1.8f, levelYPos.Mid));
        levelToCameraYPos.Add(levelYPos.Top, 3.20f);
        LineYPos.Add(new KeyValuePair<float, levelYPos>(1.5f, levelYPos.Top));

        ChangeToLevelView(levelYPos.Bot);
    }

    // Update is called once per frame
    public Vector3 CustomUpdate()
    {
        if (viewState == state.LevelView)
            return new Vector3(PlayerController.instance.transform.position.x, levelToCameraYPos[lastLevelPlayerWasAt], transform.position.z);
        else
            return new Vector3(-3.5f, 0f, transform.position.z);
    }

    public Vector3 ChangeToMapView()
    {
        viewState = state.MapView;
        cam.orthographicSize = 5.18f;
        return new Vector3(-3.5f, 0f, transform.position.z);
    }
    public void ChangeToLevelView(float yPos)
    {
        var level = levelYPos.Bot;
        for (int i = 0; i < LineYPos.Count; i++)
        {
            if (yPos > LineYPos[i].Key)
                level = LineYPos[i].Value;
            else
                break;
        }
        lastLevelPlayerWasAt = level;
        ChangeToLevelView(level);
    }
    public void ChangeToLevelView(levelYPos _level)
    {
        viewState = state.LevelView;
        cam.orthographicSize = 1.65f;
        transform.position = new Vector3(PlayerController.instance.transform.position.x, levelToCameraYPos[_level], transform.position.z);
    }
}

public enum levelYPos { Bot, Mid, Top }