using System.Collections;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour {
    private Light spotlight = null;

    private TextMeshProUGUI scoreText = null;
    private static int Score = 0;

    private TextMeshProUGUI winText = null;

    private AudioSource audioSource;
    public AudioClip clip;

    private void Start() {
        spotlight = GameObject
            .FindWithTag("Collectible Spotlight")
            .GetComponent<Light>();

        audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();

        var rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.up * 5f, ForceMode.Impulse);

        scoreText = GameObject
            .FindWithTag("Score")
            .GetComponent<TextMeshProUGUI>();

        winText = GameObject.FindWithTag("Win").GetComponent<TextMeshProUGUI>();
        winText.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !Gode.IsPaused) {
            Debug.Log("Look Ma, I'm colliding with the player!");

            var renderer = GetComponent<Renderer>();
            renderer.enabled = false;

            var colliders = GetComponentsInChildren<Collider>();
            foreach (var c in colliders)
                c.enabled = false;
            
            audioSource.PlayOneShot(clip);

            UpdateScore();
            StartCoroutine(easeInOutSpotlight());
        }
    }

    private void UpdateScore() {
        Score+=5;
        scoreText.text = $"Score: {Score}/5";

        if (Score == 5) {
            winText.enabled = true;
            Gode.IsPaused = true;
            Gode.victory();
            Score = 0;
        }
    }

    private IEnumerator easeInOutSpotlight() {
        const float duration = 0.25f;
        var startTime = Time.time;

        while (Time.time < startTime + duration) {
            var t = (Time.time - startTime) / duration;
            spotlight.intensity = Mathf.Lerp(0f, 2.2f, t);
            Debug.Log("Look Ma, I'm easing in the spotlight!");

            yield return null;
        }

        startTime = Time.time;

        while (Time.time < startTime + duration) {
            var t = (Time.time - startTime) / duration;
            spotlight.intensity = Mathf.Lerp(2.2f, 0f, t);
            Debug.Log("Look Ma, I'm easing out the spotlight!");

            yield return null;
        }

        spotlight.intensity = 0f;
        Destroy(gameObject);
    }
}
