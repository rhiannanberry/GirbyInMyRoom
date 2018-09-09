using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
	public static Interactable inRange;

	public GameObject UIprefab;

	public InteractionManager interactionManager;

	private GameObject bigPanel, rightPanel, leftPanel;
	private bool triggered = false;

	private Transform ui;

	// Use this for initialization
	public void Start () {
		Debug.Log("START");
		if (transform.childCount > 0) {
			ui = transform.Find(UIprefab.name);
			if ( ui != null ) {
				ui.gameObject.SetActive(true);
				bigPanel = ui.Find("BigPanel").gameObject;
				rightPanel = ui.Find("RightPanel").gameObject;
				leftPanel = ui.Find("LeftPanel").gameObject;
			}
		}
		if (interactionManager == null)
			interactionManager = new InteractionManager();
		System.Func<bool> cond = delegate {
			Debug.Log("CHECKKINGG!!");
			return true;
		};
		System.Func<bool> action = delegate {
			Debug.Log("WORKING!!!");
			return true;
		};

		/*InteractableEvent ie = new InteractableEvent(cond, action);
		em.AddEvent(ie);

		InteractableEvent ie1 = new InteractableEvent(
			delegate {
				Debug.Log("WORKING!!!--s1");
				return true;
			}, 
			delegate {
				Debug.Log("WORKING!!!--s1");
				return true;
			});

		InteractableEvent ie2 = new InteractableEvent(
			delegate {
				Debug.Log("WORKING!!!--s2");
				return true;
			}, 
			delegate {
				Debug.Log("WORKING!!!--s2");
				return true;
			});
		em.AddSequentialEvent(ie1, ie2);*/

		

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
			rightPanel.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && inRange == transform.GetComponent<Interactable>()) {
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

	public void TriggerInteractable() {
		//idk how this is rly gonna work yet
		if (triggered) {
			PlayerController.playerController.UnlockCamera();
			CloseUI();
			triggered = false;
		} else {
			Debug.Log("INTERACTABLE TRIGGERED");
			PlayerController.playerController.LockCameraToPoint(bigPanel.transform);
			interactionManager.CheckEvents();
			rightPanel.SetActive(false);
			bigPanel.SetActive(true);
			triggered = true;
		}
	}
}
