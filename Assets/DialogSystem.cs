using UnityEngine;
using System.Collections.Generic;
using System;

public class DialogSystem : MonoBehaviour {
    public static DialogSystem instance;
    void Awake() { instance = this; }

    public Font textBoxFont;
    int currentMessage = 0;
    public float dialogeRange = 1f;
    List<MessageScript> list = new List<MessageScript>();
    DialogSetUpData setUpData;

    void Start()
    {
        setUpData = new DialogSetUpData(textBoxFont);
    }

    /// <summary>
    /// Register a script, and returns nessary data to set it up
    /// </summary>
    /// <param name="script"></param>
    public DialogSetUpData Register(MessageScript script)
    {
        list.Add(script);
        return setUpData;
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

public class DialogSetUpData
{
    public Font _font;
    public DialogSetUpData(Font font)
    {
        _font = font;
    }
}
