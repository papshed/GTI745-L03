using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip clip;

    private AudioSource audioSource;

    private TextMeshProUGUI loseText = null;
    private TextMeshProUGUI scoreText = null;
    private static int Score = 0;

    private void Start() {
        loseText = GameObject.FindWithTag("Lose").GetComponent<TextMeshProUGUI>();
        loseText.enabled = false;
        
        scoreText = GameObject
            .FindWithTag("Score")
            .GetComponent<TextMeshProUGUI>();

        audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Gode.IsPaused) {
            Debug.Log("Look Ma, I'm colliding with the player!");
            
            loseText.enabled = true;
            scoreText.enabled = false;
            audioSource.PlayOneShot(clip);
            
            Gode.IsPaused = true;
        }
    }
}
