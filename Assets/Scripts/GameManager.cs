using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake() {
        if (_instance != null)
            Destroy(this);
        else
            _instance = this;
    }
    #endregion

    public static GameState CurrentState;
    private int leftScore = 0, rightScore = 0;

    [SerializeField] GameObject UICanvas, floatingTxtObj;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] int maxScore;
    public Animator CameraAnimator => cameraAnimator;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI leftScoreText;
    [SerializeField] TextMeshProUGUI rightScoreText;
    [SerializeField] TextMeshProUGUI floatingText;
    [SerializeField] TextMeshProUGUI leftWinText;
    [SerializeField] TextMeshProUGUI rightWinText;

    [Header("In-game objects")]
    [SerializeField] Ball ball;
    [SerializeField] Racket leftPlayer, rightPlayer;

    private Vector3 dfLeftPos, dfRightPos;
    const string pauseMsg = "PAUSED\nPress 'Space' to continue";
    const string normalMsg = "Press 'Space' to play";

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = GameState.Starting;
        //Time.timeScale = 0f;
        dfLeftPos = leftPlayer.transform.position;
        dfRightPos = rightPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && CurrentState == GameState.Paused) {
            ContinueGame();
        }

        if (DetectWinner() != 0)
            EndGame();
    }

    public void ScoredLeft() {
        leftScore++;
        ball.ResetBall();
    }
    public void ScoredRight() {
        rightScore++;
        ball.ResetBall();
    }
    private void PauseGame() {
        floatingText.text = pauseMsg;
        floatingTxtObj.SetActive(true);
        Time.timeScale = 0f;
        CurrentState = GameState.Paused;
    }
    private void ContinueGame() {
        floatingText.text = normalMsg;
        floatingTxtObj.SetActive(false);
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
    }
    public void ReturnMainMenu() {
        Time.timeScale = 1f;
        GameManager.CurrentState = GameState.Starting;
        cameraAnimator.SetTrigger("Back");
        UICanvas.SetActive(false);
        ball.gameObject.SetActive(false);
        leftPlayer.gameObject.SetActive(false);
        rightPlayer.gameObject.SetActive(false);
    }
    public void RestartGame() {
        ball.gameObject.SetActive(true);
        ball.ResetBall();

        leftPlayer.gameObject.SetActive(true);
        leftPlayer.transform.position = dfLeftPos;

        rightPlayer.gameObject.SetActive(true);
        rightPlayer.transform.position = dfRightPos;

        leftScore = rightScore = 0;
        floatingText.text = normalMsg;

        Color c = leftWinText.color;
        c.a = 0f; leftWinText.color = c;
        c = rightWinText.color;
        c.a = 0f; rightWinText.color = c;
    }
    //Return 0 if no one won, -1 for the left, 1 for the right
    private int DetectWinner() {
        if (leftScore >= maxScore && leftScore - rightScore > 1)
            return -1;
        if (rightScore >= maxScore && rightScore - leftScore > 1)
            return 1;
        return 0;
    }
    private void EndGame() {
        Time.timeScale = 0f;
        floatingTxtObj.SetActive(false);
        if (DetectWinner() == -1) {
            Color c = leftWinText.color;
            c.a = 1f; leftWinText.color = c;
        }
        else {
            Color c = rightWinText.color;
            c.a = 1f; rightWinText.color = c;
        }
    }
}

public enum GameState
{
    Starting, Paused, Playing
}