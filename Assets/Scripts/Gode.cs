using TMPro;
using UnityEngine;

public class Gode : MonoBehaviour
{
    private Gode instance = null;

    private TextMeshProUGUI winText = null;
    private TextMeshProUGUI loseText = null;
    private TextMeshProUGUI scoreText = null;
    private static int Score = 0;

    public RectTransform menu = null;

    public static bool IsPaused { get; set; } = false;

    private void Start() {
        if (!instance) this.instance = this;
        else Destroy(this);

        winText = GameObject.FindWithTag("Win").GetComponent<TextMeshProUGUI>();
        loseText = GameObject.FindWithTag("Lose").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();

        winText.enabled = false;
        loseText.enabled = false;
        scoreText.enabled = true;

        menu.localScale = Vector3.zero;
    }

    public void Play() {
        IsPaused = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void Pause() {
        IsPaused = true;
        menu.localScale = Vector3.one;
    }

    public void Tutoriel() {
        // TODO
    }

    public void Credits() {
        // TODO
    }
}
