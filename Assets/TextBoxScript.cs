using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxScript : MonoBehaviour {

    public Text text;

    // Use this for initialization
    public void SetText(string s)
    {
        text.text = s.ToString();
    }
}
