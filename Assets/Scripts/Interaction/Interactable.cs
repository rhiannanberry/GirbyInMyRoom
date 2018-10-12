using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
	public static Interactable inRange;

	protected bool collisionBasedTrigger = true;

	public InteractionManager interactionManager;

	private GameObject bigPanel, rightPanel, leftPanel;
	private bool triggered = false;

	private Transform ui;

	// Use this for initialization
	public void Start () {
		Debug.Log("START");
		if (transform.childCount > 0) {
			ui = transform.Find("WorldUICanvas");
			if ( ui != null ) {
				ui.gameObject.SetActive(true);
				bigPanel = ui.Find("BigPanel").gameObject;
				rightPanel = ui.Find("RightPanel").gameObject;
				leftPanel = ui.Find("LeftPanel").gameObject;
			}
		} else {
			Debug.Log("This interactable does not contain a UIPrefab.");
		}
		if (interactionManager == null)
			interactionManager = new InteractionManager();
		

		CloseUI();
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) {
			Debug.Log("In range!!");
			inRange = transform.GetComponent<Interactable>();
			if (ui != null)
				rightPanel.SetActive(true);
			if (collisionBasedTrigger)
				TriggerInteractable();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && inRange == transform.GetComponent<Interactable>()) {
			if (ui != null)
				rightPanel.SetActive(false);

			inRange = null;
		}
	}

	private void CloseUI() {
		if ( ui != null ) {
			bigPanel.SetActive(false);
			rightPanel.SetActive(false);
			leftPanel.SetActive(false);
		}
	}

    //Check if text is finished typing, if not then complete the text
    private bool CheckTextStatus()
    {
        if (!bigPanel.activeInHierarchy)
        {
            return false;
        }
        if (bigPanel.GetComponentInChildren<TextEffects>().StillTyping())
        {
            bigPanel.GetComponentInChildren<TextEffects>().FinishText();
            return true;
        }
        return false;
    }

    public void TriggerInteractable() {
        //idk how this is rly gonna work yet
        if (triggered) {
            if (!CheckTextStatus()) { 
                if (interactionManager.IsCurrEventEnd())
                {
                    PlayerController.playerController.UnlockCamera();
                    CloseUI();
                    triggered = false;
                } else
                {
                    Debug.Log("INTERACTABLE TRIGGERED");
                    interactionManager.CheckEvents();
                }
            }
		} else {
			Debug.Log("INTERACTABLE TRIGGERED");
			interactionManager.CheckEvents();
			if (ui != null) {
				PlayerController.playerController.LockCameraToPoint(bigPanel.transform);
				rightPanel.SetActive(false);
				bigPanel.SetActive(true);
			}
			triggered = true;
		}
	}
}
