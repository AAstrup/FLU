using UnityEngine;
using System.Collections.Generic;
using System;

public class MessageScript : MonoBehaviour {

    bool active = false;
    string lastMessagePrinted = "";
    TextBoxScript textBox;
    TextBox_Trigger textBoxTrigger;
    public Vector2 textBoxOffset;
    public Vector2 textBoxTriggerOffset;
    [SerializeField] levelYPos level;
    
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

        textBox.gameObject.SetActive(false);
        textBoxTrigger.gameObject.SetActive(false);
    }

    public void Appear(int currentMessage)
    {
        if (Messages.Count == 0)
            return;

        if (currentMessage < 0)//TextboxTrigger
        {
            textBox.gameObject.SetActive(false);
            textBoxTrigger.gameObject.SetActive(true);
            textBoxTrigger.transform.position = transform.position + new Vector3(textBoxTriggerOffset.x, textBoxTriggerOffset.y, 0f);
        }
        else if(currentMessage >= 0)//Textbox
        {
            textBox.gameObject.SetActive(true);
            textBoxTrigger.gameObject.SetActive(false);
            if (Messages[Math.Min(Messages.Count - 1, currentMessage)].message.ToString() != lastMessagePrinted)
            {
                lastMessagePrinted = Messages[Math.Min(Messages.Count - 1, currentMessage)].message.ToString().ToUpper();
                active = true;
                textBox.SetText(lastMessagePrinted);
                textBox.transform.position = transform.position + new Vector3(textBoxOffset.x, textBoxOffset.y, 0f);
            }
        }
        else
        {
            textBox.gameObject.SetActive(false);
            textBoxTrigger.gameObject.SetActive(false);
        }
    }

    public void DeActivate()
    {
        textBox.gameObject.SetActive(false);
        textBoxTrigger.gameObject.SetActive(false);
        lastMessagePrinted = "";
        active = false;
    }

    [System.Serializable]
    public class Message
    {
        public string message;
    }
    
    public List<Message> Messages = new List<Message>();
}
