using UnityEngine;
using System.Collections;
using System;

public class RespawnSystem : MonoBehaviour {
    public static RespawnSystem instance;
    void Awake() { instance = this; }

    Vector3 respawnPos;
    public float deathYDistance = -5.2f;

	// Use this for initialization
	void Start () {
        respawnPos = PlayerController.instance.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerController.instance.transform.position.y < deathYDistance)
            Respawn();
	}

    private void Respawn()
    {
        PlayerController.instance.Reset();
        PlayerController.instance.transform.position = respawnPos;
        CameraScript.instance.ChangeToLevelView(PlayerController.instance.transform.position.y);
    }

    public void UpdateRespawnPosition(Vector2 pos)
    {
        respawnPos = pos;
    }
}
