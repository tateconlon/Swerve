using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    const float startDelay = 1;

    public int score = 0;
    public int highScore = -1;
    public bool gameOver;

    float delayTime;

    public PlayerController player;
    public PlatformManager platManager;
    public Text scoreText;
    public Text finalScoreText;
    public Text highScoreText;
    public Text newHighScoreText;
    public Text titleText;
    public Text tapText;
    public AudioSource music;
    public AudioSource horn;

    bool first;

	// Use this for initialization
	void Awake () {
        if (gm == null)
        {
            gm = this;
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        delayTime = startDelay;
        newHighScoreText.enabled = false;
        EnableTexts();
        finalScoreText.enabled = false;
        gameOver = true;
        first = true;
        score = 0;
        AddScore(0);
    }
	
	// Update is called once per frame
	void Update () {
        music.pitch = 1.35f + score / 200f;
        delayTime -= Time.deltaTime;
        bool touch = false;
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                touch = true;
                break;
            }
        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)|| touch) && gameOver && delayTime<=0)
        {
            Debug.Log("Touch");
            if (first)
            {
                StartGame();
            }
            else
            {
                Reset();
                StartGame();
            }
        }
	}

    void Reset()
    {
        player.Reset();
        platManager.Reset();
    }

    void StartGame()
    {
        AddScore(-score);
        gameOver = false;
        first = false;
        DisableTexts();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        if (score > highScore) {
            highScoreText.GetComponent<Animator>().SetBool("gameOver", true);   //Flash
            highScoreText.text = "HS: " + score;
        }
    }

    public void GameOver()
    {
        horn.Play();
        gameOver = true;
        if (score > highScore)
        {
            NewHighScore();
        }
        finalScoreText.text = "SCORE: " + score;
        EnableTexts();
        delayTime = startDelay;
    }

    void EnableTexts()
    {
        titleText.enabled = true;
        tapText.enabled = true;
        finalScoreText.enabled = true;
    }

    void DisableTexts()
    {
        titleText.enabled = false;
        tapText.enabled = false;
        highScoreText.GetComponent<Animator>().SetBool("gameOver", false);
        newHighScoreText.GetComponent<Animator>().SetBool("gameOver", false);
        newHighScoreText.enabled = false;
        finalScoreText.enabled = false;
    }

    void NewHighScore()
    {
        highScore = score;
        highScoreText.GetComponent<Animator>().SetBool("gameOver", true);
        newHighScoreText.enabled = true;
        newHighScoreText.GetComponent<Animator>().SetBool("gameOver", true);
    }
}
