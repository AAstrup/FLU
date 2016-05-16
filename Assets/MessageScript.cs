using UnityEngine;
using System.Collections.Generic;
using System;

public class MessageScript : MonoBehaviour {

    dialogState state = dialogState.Silent;
    enum dialogState { Silent, ExclamationPointBox,Talking }
    TextBoxScript textBox;
    TextBox_Trigger textBoxTrigger;
    public Vector2 textBoxOffset;
    public Vector2 textBoxTriggerOffset;
    [SerializeField] levelYPos level;
    int currentMessage = 0;
    float talkDistance = 1f;

    void Start()
    {
        var GMJ_textBox = Instantiate(Resources.Load("TextBox"), transform.position + new Vector3(textBoxOffset.x, textBoxOffset.y,0f), Quaternion.identity) as GameObject;
        GMJ_textBox.transform.SetParent(GameObject.Find("GUI_Overlay").transform);
        textBox = GMJ_textBox.GetComponent<TextBoxScript>();

        var gmjTrigger = Instantiate(Resources.Load("TextBoxTrigger"), transform.position + new Vector3(textBoxTriggerOffset.x, textBoxTriggerOffset.y, 0f), Quaternion.identity) as GameObject;
        gmjTrigger.transform.SetParent(GameObject.Find("GUI_Overlay").transform);
        textBoxTrigger = gmjTrigger.GetComponent<TextBox_Trigger>();

        var dialogSetUpData = DialogSystem.instance.Register(this, level);
        textBox.SetUp(dialogSetUpData);
        textBoxTrigger.SetUp(dialogSetUpData);
        SetUp(dialogSetUpData);

        textBox.gameObject.SetActive(false);
        textBoxTrigger.gameObject.SetActive(false);
    }

    private void SetUp(DialogSetUpData dialogSetUpData)
    {
        talkDistance = dialogSetUpData.GetTalkDistance();
    }

    public void Talk()
    {
        if(state == dialogState.Talking)//Aleredy talking
            currentMessage++;
        state = dialogState.Talking;
        textBox.gameObject.SetActive(true);
        textBoxTrigger.gameObject.SetActive(false);
        var text = Messages[Math.Min(Messages.Count - 1, currentMessage)].message.ToString().ToUpper();
        textBox.SetText(text);
    }

    public void Appear()
    {
        if (Messages.Count == 0 || state == dialogState.Talking)
            return;
        state = dialogState.ExclamationPointBox;
        textBox.gameObject.SetActive(false);
        textBoxTrigger.gameObject.SetActive(true);
    }

    bool WithinTalkDistance()
    {
        return talkDistance >= Mathf.Abs(transform.position.x-PlayerController.instance.transform.position.x);
    }

    public void MakeSilent()
    {
        textBox.gameObject.SetActive(false);
        textBoxTrigger.gameObject.SetActive(false);
        state = dialogState.Silent;
        currentMessage = 0;
    }

    void Update()
    {
        if (state == dialogState.Talking)
        {
            textBox.transform.position = transform.position + new Vector3(textBoxOffset.x, textBoxOffset.y, 0f);
        }
        else if(state == dialogState.ExclamationPointBox)
        {
            textBoxTrigger.transform.position = transform.position + new Vector3(textBoxTriggerOffset.x, textBoxTriggerOffset.y, 0f);
        }

        if (state != dialogState.Silent)
        {
            if (!WithinTalkDistance())
                MakeSilent();
        }
    }

    [System.Serializable]
    public class Message
    {
        public string message;
    }
    
    public List<Message> Messages = new List<Message>();
}
