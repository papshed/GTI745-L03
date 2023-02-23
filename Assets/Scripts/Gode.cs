using TMPro;
using UnityEngine;

public class Gode : MonoBehaviour
{
    private Gode instance = null;

    private TextMeshProUGUI winText = null;
    private TextMeshProUGUI scoreText = null;
    
    private TextMeshProUGUI loseText = null;
    public RectTransform loseButton = null;

    public RectTransform menu = null;
    public bool hideMenuAtStart = true;

    public RectTransform victoryMenu = null;
    public bool hideVictoryMenuAtStart = true;
    
    public RectTransform tutoMenu = null;
    public bool hideTutoMenuAtStart = true;

    public RectTransform creditsMenu = null;
    public bool hideCreditsMenuAtStart = true;

    public bool canPause = false;

    public static bool IsPaused { get; set; } = false;
    public static bool victoire { get; set; } = false;

    private void Start() {
        if (!instance) instance = this;
        else Destroy(this);

        winText = GameObject.FindWithTag("Win")?.GetComponent<TextMeshProUGUI>();
        loseText = GameObject.FindWithTag("Lose")?.GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindWithTag("Score")?.GetComponent<TextMeshProUGUI>();

        if (winText)
            winText.enabled = false;

        if (loseText) {
            loseText.enabled = false;
            loseButton.localScale = Vector3.one;
        }
        
        if (loseButton) {
            loseButton.localScale = Vector3.zero;
        }

        if (scoreText)
            scoreText.enabled = true;

        if (menu && hideMenuAtStart)
            menu.localScale = Vector3.zero;
        
        if (victoryMenu && hideVictoryMenuAtStart)
            victoryMenu.localScale = Vector3.zero;
        
        if (tutoMenu && hideTutoMenuAtStart)
            tutoMenu.localScale = Vector3.zero;
        
        if (creditsMenu && hideCreditsMenuAtStart)
            creditsMenu.localScale = Vector3.zero;
    }

    private void Update() {
        if (victoryMenu && victoire) {
            IsPaused = true;
            menu.localScale = Vector3.zero;
            victoryMenu.localScale = Vector3.one;
        } else if (canPause && Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused) {
                IsPaused = false;
                menu.localScale = Vector3.zero;
            } else {
                IsPaused = true;
                menu.localScale = Vector3.one;
            }
        }
    }

    public void @continue() {
        IsPaused = false;
        menu.localScale = Vector3.zero;
    }

    public static void victory() {
        victoire = true;
    }

    public void enterTuto() {
        menu.localScale = Vector3.zero;
        tutoMenu.localScale = Vector3.one;
    }
    
    public void exitTuto() {
        menu.localScale = Vector3.one;
        tutoMenu.localScale = Vector3.zero;
    }

    public void enterCredits() {
        menu.localScale = Vector3.zero;
        creditsMenu.localScale = Vector3.one;
    }

    public void exitCredits() {
        menu.localScale = Vector3.one;
        creditsMenu.localScale = Vector3.zero;
    }

    public void loadMenu() {
        IsPaused = false;
        victoire = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void loadGame() {
        IsPaused = false;
        victoire = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void loadTutoriel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutoriel");
    }

    public void loadCredits() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
}
