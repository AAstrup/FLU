using UnityEngine;
using System.Collections.Generic;
using System;

public class DialogSystem : MonoBehaviour {
    public static DialogSystem instance;
    void Awake() { instance = this;
        SetUp();
    }

    void SetUp()
    {
        for (int i = 0; i < inspectorListToAdd.Count; i++)
        {
            setUpData.Add(inspectorListToAdd[i].level, inspectorListToAdd[i]);
        }
    }

    int currentMessage = -1;//-1 as you have to press in order to make the first bubble appear
    List<MessageScript> list = new List<MessageScript>();
    Dictionary<levelYPos, DialogSetUpData> setUpData = new Dictionary<levelYPos, DialogSetUpData>();
    //Set in inspector
    public Font textBoxFont;
    public float dialogeRange = 1f;
    [SerializeField] protected
    List<DialogSetUpData> inspectorListToAdd;

    /// <summary>
    /// Register a script, and returns nessary data to set it up
    /// </summary>
    /// <param name="script"></param>
    public DialogSetUpData Register(MessageScript script, levelYPos level)
    {
        list.Add(script);
        return setUpData[level];
    }

    public void TalkPlayerInput()
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
                msg.Appear(currentMessage);
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
        currentMessage = -1;
    }
}

[System.Serializable]
public class DialogSetUpData
{
    public levelYPos level;
    public Font _font;
    public Color _fontColor;
    public Sprite _textBoxSprite;
    public Sprite _textBoxTriggerSprite;
    public DialogSetUpData(Font font,Sprite textBoxSprite,Color fontColor,Sprite textBoxTriggerSprite)
    {
        _font = font;
        _textBoxSprite = textBoxSprite;
        _fontColor = fontColor;
        _textBoxTriggerSprite = textBoxTriggerSprite;
    }
}
