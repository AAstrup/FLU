using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBox_Trigger : MonoBehaviour {

    public Image image;
	// Update is called once per frame
	public void SetUp (DialogSetUpData data) {
        image.sprite = data._textBoxTriggerSprite;
    }
}
