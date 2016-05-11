using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraScript : MonoBehaviour {
    public static CameraScript instance;
    void Awake() { instance = this; }

    [SerializeField] protected string inputKey = "e";
    [SerializeField] protected state viewState = state.LevelView;
    public enum state { LevelView,MapView }
    public enum levelYPos { Bot,Mid,Top }
    Dictionary<levelYPos, float> levelToYPos = new Dictionary<levelYPos, float>();
    Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        levelToYPos.Add(levelYPos.Bot, -3.45f);
        levelToYPos.Add(levelYPos.Mid, -0.15f);
        levelToYPos.Add(levelYPos.Top, 3.20f);

        ChangeToLevelView(GetCurrentLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (viewState == state.LevelView)
            transform.position = new Vector3(PlayerController.instance.transform.position.x, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(inputKey))
            ChangeView();
    }

    public void ChangeView()
    {
        if (viewState == state.LevelView)
            ChangeToMapView();
        else
            ChangeToLevelView(GetCurrentLevel());
    }

    private levelYPos GetCurrentLevel()
    {
        return levelYPos.Bot;
    }

    public void ChangeToMapView()
    {
        viewState = state.MapView;
        cam.orthographicSize = 5.18f;
        transform.position = new Vector3(-3.5f, 0f, transform.position.z);
    }
    public void ChangeToLevelView(levelYPos _level)
    {
        viewState = state.LevelView;
        cam.orthographicSize = 1.65f;
        transform.position = new Vector3(PlayerController.instance.transform.position.x, levelToYPos[_level], transform.position.z);
    }
}
