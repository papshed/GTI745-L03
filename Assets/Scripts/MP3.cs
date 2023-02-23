using UnityEngine;

public class Mp3 : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
}
