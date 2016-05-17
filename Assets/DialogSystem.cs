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
            inspectorListToAdd[i].SetTalkDistance(dialogeRange);
        }
    }

    Dictionary<levelYPos, DialogSetUpData> setUpData = new Dictionary<levelYPos, DialogSetUpData>();//Set in inspector
    Dictionary<levelYPos, List<MessageScript>> messageScripts = new Dictionary<levelYPos, List<MessageScript>>();//whats checked in runtime
    //Set in inspector
    public float dialogeRange = 1f;
    [SerializeField] protected
    List<DialogSetUpData> inspectorListToAdd;
    [SerializeField]
    protected string talkButtonLetter;

    /// <summary>
    /// Register a script, and returns nessary data to set it up
    /// </summary>
    /// <param name="script"></param>
    public DialogSetUpData Register(MessageScript script, levelYPos level)
    {
        if(!messageScripts.ContainsKey(level))
            messageScripts.Add(level, new List<MessageScript>());
        messageScripts[level].Add(script);
        return setUpData[level];
    }

    void Update()
    {
        if (PlayerController.instance.IsFlying())
            return;
        foreach (var msg in messageScripts[CameraScript.instance.GetLastPlayerLevelPosition()])
        {
            if ( dialogeRange >= Mathf.Abs(msg.transform.position.x - PlayerController.instance.transform.position.x))
            {
                msg.Appear();
                if (Input.GetKeyDown(talkButtonLetter))
                    msg.Talk();
            }
        }
    }
}

[System.Serializable]
public class DialogSetUpData
{
    private float _talkDistance;
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

    public float GetTalkDistance()
    {
        return _talkDistance;
    }
    public void SetTalkDistance(float talkDistance)
    {
        _talkDistance = talkDistance;
    }
}
