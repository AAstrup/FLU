using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxScript : MonoBehaviour {

    public Text textBoxText;

    public void SetUp(Font font)
    {
        textBoxText.font = font;
    }
    // Use this for initialization
    public void SetText(string s)
    {
        textBoxText.text = s.ToString();
    }
}
