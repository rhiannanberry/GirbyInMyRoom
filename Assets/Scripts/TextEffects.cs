using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffects : MonoBehaviour {

	public int typeSpeed = 20;
	public string text;

	TextMeshProUGUI tm;

	private int textLength;
	private int textIndexPosition = 0;

	// Use this for initialization
	void Start () {	
		
	}
	void Awake()
	{
		tm = GetComponent<TextMeshProUGUI>();
		if (text.Length == 0) {
			text = tm.text;
		}
	}

	void OnEnable() {
		textIndexPosition = 0;
		tm.text = "";
		textLength = text.Length;
	}

	public void ChangeTypeSpeed(int speed) {
		typeSpeed = speed;
	}

    public bool StillTyping()
    {
        Debug.Log("still typing: " + (textIndexPosition < textLength));
        return textIndexPosition < textLength;
    }
    public void FinishText()
    {
        CancelInvoke();
        tm.SetText(text);
        tm.ForceMeshUpdate();
        textIndexPosition = textLength;
    }

    public bool IsOverflowing() {
        if (tm.textInfo.pageCount == tm.pageToDisplay) {
            return false;
        } else {
            tm.text = "";
            text = text.Substring(tm.firstOverflowCharacterIndex, text.Length - tm.firstOverflowCharacterIndex);
            textIndexPosition = 0;
            InvokeRepeating("UpdateText", 0f, 1.0f / typeSpeed);
            return true;
        }
        
    }

    public void NextPage()
    {
        tm.pageToDisplay++;
    }

    public void TriggerText(string txt) {
		text = txt;
		textIndexPosition = 0;
		tm.text = "";
        //tm.pageToDisplay = 1;
		textLength = text.Length;
		TriggerText();
	}

	public void TriggerText() {
		InvokeRepeating("UpdateText", 0f, 1.0f/typeSpeed);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void UpdateText() {
		if (!gameObject.activeInHierarchy) {
			CancelInvoke();
			return;
		}

        //Debug.Log(tm.textInfo.lineLength);
        //tm.SetText(text.Substring(0, textIndexPosition));
        tm.text += text[textIndexPosition];
        
        

		textIndexPosition++;
		if (textIndexPosition == textLength) {
			CancelInvoke();
		}
	}
}
