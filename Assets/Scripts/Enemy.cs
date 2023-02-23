using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip clip;
    public RectTransform loseButton = null;

    private AudioSource audioSource;

    private TextMeshProUGUI loseText = null;
    private TextMeshProUGUI scoreText = null;

    private void Start() {
        loseText = GameObject.FindWithTag("Lose").GetComponent<TextMeshProUGUI>();
        loseText.enabled = false;
        
        if (loseButton) {
            loseButton.localScale = Vector3.zero;
        }
        
        scoreText = GameObject
            .FindWithTag("Score")
            .GetComponent<TextMeshProUGUI>();

        audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Gode.IsPaused) {
            Debug.Log("Look Ma, I'm colliding with the player!");
            
            if (loseButton) {
                loseButton.localScale = Vector3.one;
            }
            
            loseText.enabled = true;
            scoreText.enabled = false;
            audioSource.PlayOneShot(clip);
            
            Gode.IsPaused = true;
        }
    }
}
