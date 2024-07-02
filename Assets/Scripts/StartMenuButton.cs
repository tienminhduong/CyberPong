using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuButton : MonoBehaviour
{
    [SerializeField] List<GameObject> ingameObjects = new List<GameObject>();

    [SerializeField] GameObject buttonPanel;
    [SerializeField] GameObject howToPlayPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressStart() {
        Time.timeScale = 1.0f;
        GameManager.Instance.CameraAnimator.SetTrigger("Play");
        GameManager.CurrentState = GameState.Paused;
        GameManager.Instance.RestartGame();
        gameObject.SetActive(false);
    }

    public void ShowHowToPlay() {
        buttonPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void ExitHowToPlay() {
        howToPlayPanel.SetActive(false);
        buttonPanel.SetActive(true);
    }

    public void QuitButton() {
        Application.Quit();
    }
}
