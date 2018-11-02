using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle menuToggle;
	private float timeScaleRef = 1f;
    private float volumeRef = 1f;
    private bool paused;


    void Awake()
    {
        menuToggle = GetComponent <Toggle> ();
	}

    void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
		    menuToggle.isOn = !menuToggle.isOn;
            Cursor.visible = menuToggle.isOn;//force the cursor visible if anythign had hidden it
		}
	}

    private void MenuOn ()
    {
        timeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        volumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        paused = true;
    }


    public void MenuOff ()
    {
        Time.timeScale = timeScaleRef;
        AudioListener.volume = volumeRef;
        paused = false;
    }


    public void OnMenuStatusChange ()
    {
        if (menuToggle.isOn && !paused)
        {
            MenuOn();
        }
        else if (!menuToggle.isOn && paused)
        {
            MenuOff();
        }
    }


	

}
