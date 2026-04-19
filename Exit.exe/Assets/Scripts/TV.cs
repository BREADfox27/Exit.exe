using UnityEngine;
using TMPro;
public class TV : MonoBehaviour

{
    public GameObject interactionText;     // Texto: "Leer [B]"
    public GameObject instructionsPanel;   // Panel con instrucciones

    private bool playerNear = false;
    private bool panelOpen = false;

    private void Start()
    {
        interactionText.SetActive(false);
        instructionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.B))
        {
            panelOpen = !panelOpen;
            instructionsPanel.SetActive(panelOpen);

            // Opcional: ocultar el texto de "Leer [B]" cuando el panel esté abierto
            interactionText.SetActive(!panelOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;

            if (!panelOpen)
                interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            interactionText.SetActive(false);
            instructionsPanel.SetActive(false);
            panelOpen = false;
        }
    }
}

