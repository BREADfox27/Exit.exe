using UnityEditor;
using UnityEngine;

public class BringupSettings : MonoBehaviour
{
    public GameObject settings;
    public GameObject otherCanvas;
    public bool isSettingsActive;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingsActive == false)
            {
                Pause();
            }

            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        settings.SetActive(true);
        otherCanvas.SetActive(false);
        isSettingsActive = true;
        this.GetComponent<CameraRotation>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        settings.SetActive(false);
        otherCanvas.SetActive(true);
        isSettingsActive = false;
        this.GetComponent<CameraRotation>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
