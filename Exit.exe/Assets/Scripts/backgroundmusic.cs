using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioSource audioSource; // Arrastra aquí el componente AudioSource
    public AudioClip musicClip;     // Arrastra aquí tu archivo de música

    void Start()
    {
        // Configurar el AudioSource
        if (audioSource != null && musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true; // Fundamental para música de fondo
            audioSource.playOnAwake = true;
            audioSource.Play();
        }
    }
}