using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxScript : MonoBehaviour {

    public Image image;
    public Text textBoxText;

    public void SetUp(DialogSetUpData setUpData)
    {
        textBoxText.font = setUpData._font;
        textBoxText.color = setUpData._fontColor;
        image.sprite = setUpData._textBoxSprite;
    }
    // Use this for initialization
    public void SetText(string s)
    {
        textBoxText.text = s.ToString();
    }
}
