using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int nightmaresDefeated;

    public GameObject nightmare;
    public bool GameInPlay;
    public Text scoreText;

    public AudioClip hurtSound; // Drag your hurt .wav file here in the Inspector


    void Start()
    {
        nightmaresDefeated = 0;
        GameInPlay = false;

        // delete this line later!!!
        //startGame();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This runs automatically every time a new scene finishes loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            // Re-link your UI and scene objects here!
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
            {
                scoreText = scoreObj.GetComponent<Text>(); // Use TMP_Text if using TextMeshPro
                scoreText.text = nightmaresDefeated.ToString();
            }
            GameInPlay = true;
        }
        if (scene.name == "RestartGame") { 
            GameObject.Find("score").GetComponent<Text>().text = "You saved " + nightmaresDefeated.ToString() + " nightmares!";
        
        }
    }

    public void winGame() {
        nightmaresDefeated += 1;

        AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position);

        GameInPlay = false;

        StartCoroutine(slowSwitchScene("MainGame"));

    }
    public void loseGame()
    {
        GameInPlay = false;
        StartCoroutine(slowSwitchScene("RestartGame"));

    }

    IEnumerator slowSwitchScene(string sceneName) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void startGame()
    {
        Debug.Log("Button clicked! Starting game...");
        Time.timeScale = 1f;
        nightmaresDefeated = 0;
        GameInPlay = true;
        StartCoroutine(slowSwitchScene("MainGame"));

        ParticleSystem particles = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        if (particles != null)
        {
            particles.startLifetime = 8f;
            particles.startSpeed = 5f;
        }
        else
        {
            Debug.LogWarning("StartParticles not found!");
        }

    }
}
