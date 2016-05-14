using UnityEngine;
using System.Collections.Generic;
using System;

public class MessageScript : MonoBehaviour {

    bool active = false;
    string lastMessagePrinted = "";
    public GameObject prefab;
    TextBoxScript textBox;
    public Vector2 textBoxOffset;
    [SerializeField] levelYPos level;

    void Start()
    {
        var gmj = Instantiate(prefab, transform.position + new Vector3(textBoxOffset.x, textBoxOffset.y,0f), Quaternion.identity) as GameObject;
        gmj.transform.SetParent(GameObject.Find("GUI_Overlay").transform);
        textBox = gmj.GetComponent<TextBoxScript>();

        var dialogSetUpData = DialogSystem.instance.Register(this, level);
        textBox.SetUp(dialogSetUpData);
    }

    public void Appear(int currentMessage)
    {
        if (Messages.Count == 0)
            return;
        textBox.gameObject.SetActive(true);
        if (Messages[Math.Min(Messages.Count-1, currentMessage)].message.ToString() != lastMessagePrinted)
        {
            lastMessagePrinted = Messages[Math.Min(Messages.Count-1, currentMessage)].message.ToString().ToUpper();
            active = true;
            textBox.SetText(lastMessagePrinted);
            textBox.transform.position = transform.position + new Vector3(textBoxOffset.x, textBoxOffset.y, 0f);
        }
    }

    public void DeActivate()
    {
        textBox.gameObject.SetActive(false);
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
