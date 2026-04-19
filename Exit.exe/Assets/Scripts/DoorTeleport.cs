using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorTeleport : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionText;   // Texto: Open [P]
    public Image fadePanel;              // Imagen negra del Canvas

    [Header("Jugador")]
    public Transform player;
    public Transform teleportPoint;

    [Header("Control")]
    public KeyCode interactKey = KeyCode.P;
    public float fadeDuration = 1f;

    private bool playerNear = false;
    private bool hasTeleported = false;
    private bool isTransitioning = false;

    void Start()
    {
        if (interactionText != null)
            interactionText.SetActive(false);

        if (fadePanel != null)
        {
            Color c = fadePanel.color;
            c.a = 0f;
            fadePanel.color = c;
            fadePanel.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (playerNear && !hasTeleported && !isTransitioning && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(TeleportTransition());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTeleported && other.CompareTag("Player"))
        {
            playerNear = true;

            if (interactionText != null)
                interactionText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (interactionText != null)
                interactionText.SetActive(false);
        }
    }

    IEnumerator TeleportTransition()
    {
        isTransitioning = true;

        if (interactionText != null)
            interactionText.SetActive(false);

        // Fade a negro
        yield return StartCoroutine(Fade(0f, 1f));

        // Teletransporte
        if (player != null && teleportPoint != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
                cc.enabled = false;

            player.position = teleportPoint.position;

            if (cc != null)
                cc.enabled = true;
        }

        hasTeleported = true;
        playerNear = false;

        // Espera pequeña opcional
        yield return new WaitForSeconds(0.2f);

        // Fade desde negro
        yield return StartCoroutine(Fade(1f, 0f));

        isTransitioning = false;

        // Desactivar la puerta para que no pueda volver a usarla
        gameObject.SetActive(false);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color color = fadePanel.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadePanel.color = color;

            yield return null;
        }

        color.a = endAlpha;
        fadePanel.color = color;
    }
}

