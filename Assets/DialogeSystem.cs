using UnityEngine;
using System.Collections.Generic;
using System;

public class DialogeSystem : MonoBehaviour {
    public static DialogeSystem instance;
    void Awake() { instance = this; }

    int currentMessage = 0;
    public float dialogeRange = 1f;
    List<MessageScript> list = new List<MessageScript>();

    public void Register(MessageScript script)
    {
        list.Add(script);
    }

    public void PlayerInput()
    {
        currentMessage++;
    }

    void Update()
    {
        bool dialogActive = false;
        foreach (var msg in list)
        {
            if (Vector2.Distance(msg.transform.position, PlayerController.instance.transform.position) < dialogeRange)
            {
                msg.Activate(currentMessage);
                dialogActive = true;
            }
            else
                msg.DeActivate();
        }
        if (!dialogActive)
            ResetDialoge();
    }

    private void ResetDialoge()
    {
        currentMessage = 0;
    }
}
